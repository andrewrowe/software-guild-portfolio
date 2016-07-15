using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TravelBlogCapstone.Data;
using TravelBlogCapstone.Data.DapperRepositories;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.BLL
{
    public class EmployeeManager
    {
        private readonly IPostsRepository _postRepo;
        private readonly ICategoriesRepository _categoryRepo;
        private readonly ITagsRepository _tagRepo;

        public EmployeeManager()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _postRepo = kernel.Get<IPostsRepository>();
            _categoryRepo = kernel.Get<ICategoriesRepository>();
            _tagRepo = kernel.Get<ITagsRepository>();
        }

        public List<Post> GetAllPendingAndDraftPosts()
        {
            var posts = _postRepo.GetAll().Where(p => p.StatusId == 1).Take(3).ToList();
            return posts;
        }

        public List<Post> GetAllUserPostsIndex(string userId)
        {
            return _postRepo.GetAll().Where(p => p.UserId == userId && (p.StatusId != 1 && p.StatusId != 5)).ToList();
        }

        public Post CreateNewPost(Post post)
        {
            //Call repo Tags, add new tags if not exist
            if (post.StatusId != 2 && post.StatusId != 4)
            {
                throw new Exception();
            }
            post = _postRepo.Insert(post);
            return post;
        }

        public List<Post> GetAllApprovedPost()
        {
            var posts= _postRepo.GetAll().Where(p => p.StatusId == 1).ToList();
            return posts;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _categoryRepo.GetAll().ToList();
            return categories;
        }

        public List<Tag> GetAllTags()
        {
            var tags = _tagRepo.GetAll().ToList();
            return tags;
        }

        public void DeletePost(int postId)
        {
            if (_postRepo.Get(postId).StatusId == 5)
            {
                throw new InvalidDataException();
            }
            _postRepo.Delete(postId);
        }

        public void UpdatePost(Post post, int statusId)
        {
            //need to perform update logic based on status ID and 

           
            if (post.StatusId == (int)Status.Approved) //post was online
            {
                var originalPostId = post.Id;

                if (statusId == 1) //set as draft status
                {
                    post.StatusId = (int) Status.Draft;
                }
                else //set as pending update
                {
                    post.StatusId = (int)Status.PendingUpdate;
                }

                var newPost = _postRepo.Insert(post); //insert new row in posts

                //update original row with updatedpostID
                _postRepo.SetUpdatedPostId(originalPostId, newPost.Id);
                

            }

            //post was not online
            if (post.StatusId != (int) Status.Approved)
            {
                if (statusId == 1) //set as draft status
                {
                    post.StatusId = (int)Status.Draft;
                }
                else //set as pending new
                {
                    post.StatusId = (int)Status.PendingNew;
                }

                _postRepo.UpdatePost(post);

            }

        }

    }
}
