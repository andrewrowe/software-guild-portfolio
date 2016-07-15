using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.TestRepositories
{
    public class TestPostsRepository : IPostsRepository
    {
        private static List<Post> _posts = new List<Post>
        {
            new Post {Categories = new List<Category> {new Category { CategoryName = "Hotel", Id = 1} }, PostContent = "This post is about a hotel with ID 1.", Id = 1, Title = "Post 1: Hotel", PublishedDate = DateTime.Today, Tags = new List<Tag> {new Tag {TagName = "africa"}, new Tag {TagName = "beach"}, new Tag {TagName = "fun"} }, InitialPostDate = DateTime.Today, LastModifiedDate = DateTime.Today, Status = Status.Approved, StatusId = 1, UserId = "1"},
            new Post {Categories = new List<Category> {new Category { CategoryName = "Trip in Europe", Id = 3}, new Category {CategoryName = "Organized trip", Id = 6} }, PostContent = "This post is about a trip in Venice with ID 2.", Id = 2, Title = "Post 2: Trip in Venice", PublishedDate = DateTime.Parse("2/15/16"), Tags = new List<Tag> {new Tag {TagName = "Venice"}, new Tag {TagName = "Europe"}, new Tag {TagName = "fun"} }, InitialPostDate = DateTime.Parse("2/9/16"), LastModifiedDate = DateTime.Parse("2/8/16"), Status = Status.Approved, StatusId = 1, UserId = "3" },
            new Post {Categories = new List<Category> {new Category { CategoryName = "Restaurant", Id = 2}, new Category { CategoryName = "Trip in US", Id = 4} }, PostContent = "This post is about a trip in the US with ID 3.", Id = 3, Title = "Post 3: A Trip to the #1 Nation", PublishedDate = DateTime.Parse("4/20/16"), Tags = new List<Tag> {new Tag {TagName = "trip"}, new Tag() {TagName = "expensive"}, new Tag() {TagName = "love"}, new Tag() {TagName = "hot"} }, LastModifiedDate = DateTime.Parse("4/23/16"), ExpiredDate = DateTime.Parse("12/31/16"), Status = Status.PendingUpdate, StatusId = 3, UserId = "2"},
            new Post {Categories = new List<Category> {new Category { CategoryName = "Trip in Asia", Id = 5}, new Category { CategoryName = "Organized trip", Id = 6} }, PostContent = "This post is about a trip in Asia with ID 4.", Id = 4, Title = "Post 4: A Trip to China", PublishedDate = DateTime.Parse("3/18/16"), Tags = new List<Tag> {new Tag {TagName = "trip"}, new Tag() {TagName = "cheap"}, new Tag() {TagName = "hot"} }, LastModifiedDate = DateTime.Parse("4/23/16"), Status = Status.PendingNew, StatusId = 2, UserId = "1"},
            new Post {Categories = new List<Category> {new Category { CategoryName = "Organized trip", Id = 6} }, PostContent = "This post is about a trip to the beach with ID 5.", Id = 5, Title = "Post 5: Summer Beach Trip!", PublishedDate = DateTime.Parse("6/24/16"), Tags = new List<Tag> {new Tag {TagName = "trip"}, new Tag() {TagName = "beach"} }, LastModifiedDate = DateTime.Parse("6/23/16"), Status = Status.Approved, StatusId = 1, UserId = "2"},
            new Post {Categories = new List<Category> {new Category { CategoryName = "Restaurant", Id = 2}, new Category { CategoryName = "Trip in Europe", Id = 3} }, PostContent = "This post is about a cool restaurant in Europe with ID 6.", Id = 6, Title = "Post 6: McDonalds is in Europe!", PublishedDate = DateTime.Parse("5/19/16"), Tags = new List<Tag> {new Tag {TagName = "Europe"}, new Tag() {TagName = "cheap"}, new Tag() {TagName = "love"} }, LastModifiedDate = DateTime.Parse("5/19/16"), Status = Status.Draft, StatusId = 4, UserId = "1"},
        };

        public List<Post> GetAll()
        {
            return _posts;
        }

        public Post Get(int postid)
        {
            return _posts.FirstOrDefault(m => m.Id == postid);
        }

        public Post Insert(Post post)
        {
            _posts.Add(post);
            return post;
        }

        public void Update(Post post)
        {
            var updatePost = _posts.FirstOrDefault(m => m.Id == post.Id);

            updatePost.Title = post.Title;
            updatePost.PostContent = post.PostContent;
            updatePost.StatusId = post.StatusId;
            updatePost.LastModifiedDate = post.LastModifiedDate;
            updatePost.InitialPostDate = post.InitialPostDate;
            updatePost.UpdatedPostDate = post.UpdatedPostDate;
            updatePost.PublishedDate = post.PublishedDate;
            updatePost.ExpiredDate = post.ExpiredDate;
            updatePost.UserId = post.UserId;
            updatePost.Status = Status.Approved;
        }

        public void Delete(int postid)
        {
            var deletePost = _posts.FirstOrDefault(m => m.Id == postid);
            _posts.Remove(deletePost);
        }

        public void ApprovePost(int postid)
        {
            var approvePost = _posts.FirstOrDefault(m => m.Id == postid);
            approvePost.StatusId = 1;
            approvePost.Status = Status.Approved;
        }

        public void DisapprovePost(Post post)
        {
            var disapprovePost = _posts.FirstOrDefault(m => m.Id == post.Id);
            if (post.Remark != null)
            {
                disapprovePost.Remark = post.Remark;
            }

            disapprovePost.StatusId = 4;
            disapprovePost.Status = Status.Draft;
        }

        public List<Post> SelectPendingPosts()
        {
            var pendingPosts = _posts.Where(m => m.StatusId == 2 || m.StatusId == 3).OrderBy(m => m.LastModifiedDate).ToList();
            return pendingPosts;
        }

        public void SetUpdatedPostId(int originalPostId, int newPostId)
        {
            _posts.FirstOrDefault(m => m.Id == originalPostId).UpdatedPostId = newPostId;
        }

        public void UpdatePost(Post post)
        {
            var updatePost = _posts.FirstOrDefault(m => m.Id == post.Id);

            updatePost.Title = post.Title;
            updatePost.PostContent = post.PostContent;
            updatePost.StatusId = post.StatusId;
        }
    }
}
