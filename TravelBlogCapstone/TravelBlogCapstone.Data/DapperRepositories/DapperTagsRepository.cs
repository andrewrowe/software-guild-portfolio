using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.DapperRepositories
{
    public class DapperTagsRepository : ITagsRepository
    {
        public List<Tag> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Tags.TagName, " +
                               "Posts.ID " +
                               "FROM Tags " +
                               "LEFT JOIN Posts_Tags On Tags.TagName = Posts_Tags.TagName " +
                               "LEFT JOIN Posts ON Posts_Tags.PostID = Posts.ID ";
                var tags = new Dictionary<string, Tag>();
                cn.Query<Tag, int?, Tag>(query, (t, i) =>
                {
                    Tag tag;
                    if (!tags.TryGetValue(t.TagName, out tag))
                    {
                        tags.Add(t.TagName, tag = t);
                        tag.PostsId = new List<int>();
                    }
                    if ((i != 0) && (i != null))
                        tag.PostsId.Add((int)i);

                    return tag;
                }
                    ).AsQueryable();

                var listTags = tags.Values.ToList();
                var repo = new DapperPostsRepository();
                var allPosts = repo.GetAll();

                foreach (var tag in listTags)
                {
                    tag.Posts =
                        allPosts.Where(p => tag.PostsId.Contains(p.Id)).ToList();
                }

                return listTags;
            }
        }

        public Tag Get(string tagName)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Tags.TagName, " +
                               "Posts.ID " +
                               "FROM Tags " +
                               "LEFT JOIN Posts_Tags On Tags.TagName = Posts_Tags.TagName " +
                               "LEFT JOIN Posts ON Posts_Tags.PostID = Posts.ID " +
                               "WHERE Tags.TagName = @TagName";
                var parameters = new DynamicParameters();
                parameters.Add("TagName", tagName);
                var tags = new Dictionary<string, Tag>();

                cn.Query<Tag, int?, Tag>(query, (t, i) =>
                {
                    Tag tag;
                    if (!tags.TryGetValue(t.TagName, out tag))
                    {
                        tags.Add(t.TagName, tag = t);
                        tag.PostsId = new List<int>();
                    }
                    if ((i != 0) && (i != null))
                        tag.PostsId.Add((int)i);

                    return tag;
                }, parameters
                    ).AsQueryable();

                var tagValue = tags.Values.FirstOrDefault();

                if (tagValue != null)
                {
                    tagValue.Posts = new List<Post>();
                    var repo = new DapperPostsRepository();

                    for (int i = 0; i < tagValue?.PostsId.Count; i++)
                    {
                        tagValue.Posts.Add(repo.Get(tagValue.PostsId[i]));
                    }
                }
                return tagValue;
            }
        }

        public Tag Insert(Tag tag)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "INSERT INTO Tags(TagName) " +
                               "VALUES(@TagName); ";

                var p = new DynamicParameters();
                p.Add("TagName", tag.TagName);

                cn.Execute(query, p);
            }
            return tag;
        }

        public void Delete(string tagName)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE Tags WHERE TageName = @TagName";
                var p = new DynamicParameters();
                p.Add("TagName", tagName);
                cn.Execute(query, p);
            }
        }

        public void Update(Tag tag)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Tags SET TagName = @TagName " +
                               "FROM Tags " +
                               "WHERE TagName = @TagName";
                var p = new DynamicParameters();
                p.Add("TagName", tag.TagName);

                cn.Execute(query, p);
            }
        }
    }
}
