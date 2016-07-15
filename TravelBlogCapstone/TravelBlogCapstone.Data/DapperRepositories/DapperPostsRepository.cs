using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.DapperRepositories
{
    public class DapperPostsRepository : IPostsRepository
    {
        public List<Post> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query =
                    "SELECT Posts.ID, Posts.UpdatedPostID, Posts.Title, Posts.PostContent, Posts.LastModifiedDate, " +
                    "Posts.InitialPostDate, Posts.UpdatedPostDate, Posts.PublishedDate, Posts.ExpiredDate, Posts.UserID, " +
                    "Posts.StatusID, " +
                    "Tags.TagName, " +
                    "Categories.ID, Categories.CategoryName " +
                    "FROM Posts " +
                    "LEFT JOIN Posts_Tags ON Posts.ID = Posts_Tags.PostID " +
                    "LEFT JOIN Tags ON Posts_Tags.TagName = Tags.TagName " +
                    "LEFT JOIN Posts_Categories ON Posts.ID = Posts_Categories.PostID " +
                    "LEFT JOIN Categories ON Posts_Categories.CategoryID = Categories.ID " +
                    "Order By Posts.ID, Categories.ID";

                var posts = new Dictionary<int, Post>();
                var tags = new Dictionary<string, int>();
                var categories = new Dictionary<int, int>();

                cn.Query<Post, Tag, Category, Post>(query, (p, t, c) =>
                {
                    Post post;
                    if (!posts.TryGetValue(p.Id, out post))
                    {
                        posts.Add(p.Id, post = p);
                        post.TagsName = new List<string>();
                        post.CategoriesId = new List<int>();
                        post.Tags = new List<Tag>();
                        post.Categories = new List<Category>();
                        tags = new Dictionary<string, int>();
                        categories = new Dictionary<int, int>();
                    }
                    if ((c != null) && (!categories.ContainsKey(c.Id)))
                    {
                        categories.Add(c.Id, 1);
                        post.CategoriesId.Add(c.Id);
                        post.Categories.Add(c);
                    }
                    if ((t != null) && (!tags.ContainsKey(t.TagName)))
                    {
                        tags.Add(t.TagName, 1);
                        post.TagsName.Add(t.TagName);
                        post.Tags.Add(t);
                    }
                    return post;
                }, splitOn: "TagName,ID"
                    ).AsQueryable();

                return posts.Values.OrderBy(p => p.LastModifiedDate).ToList();
            }
        }

        public Post Get(int postId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query =
                    "SELECT Posts.ID, Posts.UpdatedPostID, Posts.Title, Posts.PostContent, Posts.LastModifiedDate, " +
                    "Posts.InitialPostDate, Posts.UpdatedPostDate, Posts.PublishedDate, Posts.ExpiredDate, Posts.UserID, " +
                    "Posts.StatusID, Posts.Remark, " +
                    "Tags.TagName, " +
                    "Categories.ID, Categories.CategoryName " +
                    "FROM Posts " +
                    "LEFT JOIN Posts_Tags ON Posts.ID = Posts_Tags.PostID " +
                    "LEFT JOIN Tags ON Posts_Tags.TagName = Tags.TagName " +
                    "LEFT JOIN Posts_Categories ON Posts.ID = Posts_Categories.PostID " +
                    "LEFT JOIN Categories ON Posts_Categories.CategoryID = Categories.ID " +
                    "WHERE Posts.ID = @PostID " +
                    "Order By Posts.ID, Categories.ID";

                var parameters = new DynamicParameters();
                parameters.Add("PostID", postId);
                var posts = new Dictionary<int, Post>();
                var tags = new Dictionary<string, int>();
                var categories = new Dictionary<int, int>();

                cn.Query<Post, Tag, Category, Post>(query, (p, t, c) =>
                {
                    Post post;
                    if (!posts.TryGetValue(p.Id, out post))
                    {
                        posts.Add(p.Id, post = p);
                        post.TagsName = new List<string>();
                        post.CategoriesId = new List<int>();
                        post.Tags = new List<Tag>();
                        post.Categories = new List<Category>();
                        tags = new Dictionary<string, int>();
                        categories = new Dictionary<int, int>();
                    }
                    if ((c != null) && (!categories.ContainsKey(c.Id)))
                    {
                        categories.Add(c.Id, 1);
                        post.CategoriesId.Add(c.Id);
                        post.Categories.Add(c);
                    }
                    if ((t != null) && (!tags.ContainsKey(t.TagName)))
                    {
                        tags.Add(t.TagName, 1);
                        post.TagsName.Add(t.TagName);
                        post.Tags.Add(t);
                    }
                    return post;
                }, parameters, 
                splitOn: "TagName,ID"
                
                    ).AsQueryable();

                return posts.Values.FirstOrDefault();
            }
        }

        public Post Insert(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                try
                {
                    cn.Open();
                    var p = new DynamicParameters();
                    if (!post.UpdatedPostId.Equals(null))
                        p.Add("UpdatedPostID", post.UpdatedPostId);
                    else
                        p.Add("UpdatedPostID", (int?)null);
                    p.Add("Title", post.Title);
                    p.Add("PostContent", post.PostContent);
                    p.Add("LastModifiedDate", DateTime.Now);
                    if (post.StatusId == 2)
                    {
                        p.Add("InitialPostDate", DateTime.Now);
                    }
                    else
                    {
                        p.Add("InitialPostDate", (DateTime?)null);
                    }
                    if (post.UpdatedPostDate != null)
                    {
                        p.Add("UpdatedPostDate", post.UpdatedPostDate);
                    }
                    p.Add("PublishedDate", post.PublishedDate);
                    if (post.ExpiredDate.Equals(null))
                    {
                        p.Add("ExpiredDate", (DateTime?)null);
                    }
                    else
                    {
                        p.Add("ExpiredDate", post.ExpiredDate);
                    }
                    p.Add("UserID", post.UserId);
                    p.Add("StatusID", post.StatusId);
                    p.Add("ID", DbType.Int32, direction: ParameterDirection.Output);

                    cn.Execute("AddNewPost", p, commandType: CommandType.StoredProcedure);

                    post.Id = p.Get<int>("ID");

                    AddPostsCategories(post.Id,post.CategoriesId);
                    AddPostsTags(post.Id,post.TagsName);

                }
                catch (Exception ex)
                {
                    // do something...
                }
               
            }
            return post;
        }

        public void Delete(int postId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Posts SET Posts.StatusID = 5 WHERE @PostID = Posts.ID";
                var p = new DynamicParameters();
                p.Add("PostID", postId);
                cn.Execute(query, p);
            }
        }

        public void Update(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Posts " +
                               "SET UpdatedPostID = @UpdatedPostID, Title = @Title, PostContent = @PostContent, " +
                               "LastModifiedDate = @LastModifiedDate, InitialPostDate = @InitialPostDate, " +
                               "UpdatedPostDate = @UpdatedPostDate, PublishedDate = @PublishedDate, " +
                               "ExpiredDate = @ExpiredDate, UserID = @UserID, StatusID = @StatusID" +
                               "WHERE ID = @PostID";

                var p = new DynamicParameters();
                p.Add("PostID", post.Id);
                if (post.UpdatedPostId != 0)
                    p.Add("UpdatedPostID", post.UpdatedPostId);
                else
                    p.Add("UpdatedPostID");
                p.Add("Title", post.Title);
                p.Add("PostContent", post.PostContent);
                p.Add("LastModifiedDate", post.LastModifiedDate);
                p.Add("InitialPostDate", post.InitialPostDate);
                p.Add("UpdatedPostDate", post.UpdatedPostDate);
                p.Add("PublishedDate", post.PublishedDate);
                p.Add("ExpiredDate", post.ExpiredDate);
                p.Add("UserID", post.UserId);
                p.Add("StatusID", post.StatusId);

                cn.Execute(query, p);
            }
        }

        public void UpdatePost(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Posts " +
                               "SET Title = @Title, PostContent = @PostContent, LastModifiedDate = @LastModifiedDate, " +
                               "StatusID = @StatusID, PublishedDate = @PublishedDate, ExpiredDate = @ExpiredDate " +
                               "WHERE ID = @PostID";

                var p = new DynamicParameters();
                p.Add("PostID", post.Id);
                
                p.Add("Title", post.Title);
                p.Add("PostContent", post.PostContent);
                p.Add("StatusID", post.StatusId);
                p.Add("PublishedDate", post.PublishedDate);
                p.Add("ExpiredDate", post.ExpiredDate);
                p.Add("LastModifiedDate", DateTime.Now);

                cn.Execute(query, p);


                DeletePostsTags(post.Id);
                DeletePostsCategories(post.Id);

                AddPostsCategories(post.Id, post.CategoriesId);
                AddPostsTags(post.Id, post.TagsName);

            }
        }

        public void ApprovePost(int postId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("PostID", postId);
                cn.Execute("ApprovePost", p, commandType: CommandType.StoredProcedure);
            }
        }

        public void DisapprovePost(Post post)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();

                p.Add("PostID", post.Id);
                p.Add("Remark", post.Remark);
                cn.Execute("DisapprovePost", p, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Post> SelectPendingPosts()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                var posts = cn.Query<Post>("SelectPendingPosts", commandType: CommandType.StoredProcedure).ToList();

                return posts;
            }
        }

        private void AddPostsCategories(int postId, List<int> categoriesId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "INSERT INTO Posts_Categories(PostID,CategoryID) VALUES (@PostID, @CategoryID)";
                foreach (var categoryId in categoriesId)
                {
                    var p = new DynamicParameters();

                    p.Add("PostID", postId);
                    p.Add("CategoryID", categoryId);
                    cn.Execute(query, p);
                }
            }
        }

        private void AddPostsTags(int postId, List<string> tagsName)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string getTagsQuery = "SELECT Tags.TagName FROM Tags";
                var allTags = cn.Query<Tag>(getTagsQuery).ToList();
                string addToTagTable = "INSERT INTO Tags(TagName) VALUES (@Name)";
                foreach (var tag in tagsName)
                {
                    if (allTags.All(t => t.TagName.ToUpper() != tag.ToUpper()))
                    {
                        var x = new DynamicParameters();
                        x.Add("Name", tag);
                        cn.Execute(addToTagTable, x);
                    }
                }

                string addTagsToBridgeQuery = "INSERT INTO Posts_Tags(PostID,TagName) VALUES (@PostID, @TagName)";
                foreach (var tagName in tagsName)
                {
                    var p = new DynamicParameters();

                    p.Add("PostID", postId);
                    p.Add("TagName", tagName);
                    cn.Execute(addTagsToBridgeQuery, p);
                }
            }
        }

        private void DeletePostsCategories(int postId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE Posts_Categories WHERE PostID = @PostId";
                var p = new DynamicParameters();
                p.Add("PostID", postId);
                cn.Execute(query, p);
            }
        }

        private void DeletePostsTags(int postId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE Posts_Tags WHERE PostID = @PostId";
                var p = new DynamicParameters();
                p.Add("PostID", postId);
                cn.Execute(query, p);
            }
        }

        public void SetUpdatedPostId(int originalPostId, int newPostId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Posts SET UpdatedPostID = @udpatedPostId WHERE ID = @PostId";
                var p = new DynamicParameters();
                p.Add("PostID", originalPostId);
                p.Add("udpatedPostId", newPostId);
                cn.Execute(query, p);
            }
        }
    }
}
