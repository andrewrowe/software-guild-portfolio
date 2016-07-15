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
    public class DapperCategoriesRepository : ICategoriesRepository
    {
        public List<Category> GetAll()
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Categories.ID, Categories.CategoryName, " +
                               "Posts.ID " +
                               "FROM Categories " +
                               "LEFT JOIN Posts_Categories On Categories.ID = Posts_Categories.CategoryID " +
                               "LEFT JOIN Posts ON Posts_Categories.PostID = Posts.ID ";
                var categories = new Dictionary<int, Category>();
                cn.Query<Category, int?, Category>(query, (c, i) =>
                {
                    Category category;
                    if (!categories.TryGetValue(c.Id, out category))
                    {
                        categories.Add(c.Id, category = c);
                        category.PostsId = new List<int>();
                    }
                    if ((i != 0) && (i != null))
                        category.PostsId.Add((int) i);

                    return category;
                }
                    ).AsQueryable();

                var listCategories = categories.Values.ToList();

                var repo = new DapperPostsRepository();
                var allPosts = repo.GetAll();

                foreach (var category in listCategories)
                {
                    category.Posts =
                        allPosts.Where(p => category.PostsId.Contains(p.Id)).ToList();
                }

                return listCategories;
            }
        }

        public Category Get(int categoryId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "SELECT Categories.ID, Categories.CategoryName, " +
                               "Posts.ID " +
                               "FROM Categories " +
                               "LEFT JOIN Posts_Categories On Categories.ID = Posts_Categories.CategoryID " +
                               "LEFT JOIN Posts ON Posts_Categories.PostID = Posts.ID " +
                               "WHERE Categories.ID = @CategoryID";

                var parameters = new DynamicParameters();
                parameters.Add("CategoryID", categoryId);
                var categories = new Dictionary<int, Category>();

                cn.Query<Category, int?, Category>(query, (c, i) =>
                {
                    Category category;
                    if (!categories.TryGetValue(c.Id, out category))
                    {
                        categories.Add(c.Id, category = c);
                        category.PostsId = new List<int>();
                    }
                    if ((i != 0) && (i != null))
                        category.PostsId.Add((int) i);

                    return category;
                }, parameters
                    ).AsQueryable();

                var categoryValue = categories.Values.FirstOrDefault();
                if (categoryValue != null)
                {
                    categoryValue.Posts = new List<Post>();
                    var repo = new DapperPostsRepository();

                    for (int i = 0; i < categoryValue?.PostsId.Count; i++)
                    {
                        categoryValue.Posts.Add(repo.Get(categoryValue.PostsId[i]));
                    }
                }
                return categoryValue;
            }
        }

        public Category Insert(Category category)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "INSERT INTO Categories(CategoryName) " +
                               "VALUES(@CategoryName); " +
                               "SET @CategoryID = SCOPE_IDENTITY();";

                var p = new DynamicParameters();
                p.Add("CategoryName", category.CategoryName);
                p.Add("CategoryID", DbType.Int32, direction: ParameterDirection.Output);

                cn.Execute(query, p);

                category.Id = p.Get<int>("CategoryID");
            }
            return category;
        }

        public void Delete(int categoryId)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "DELETE Categories WHERE ID = @CategoryID";
                var p = new DynamicParameters();
                p.Add("CategoryID", categoryId);
                cn.Execute(query, p);
            }
        }

        public void Update(Category category)
        {
            using (SqlConnection cn = new SqlConnection(Settings.ConnectionString))
            {
                string query = "UPDATE Categories SET CategoryName = @CategoryName " +
                               "FROM Categories " +
                               "WHERE ID = @CategoryID";
                var p = new DynamicParameters();
                p.Add("CategoryID", category.Id);
                p.Add("CategoryName", category.CategoryName);

                cn.Execute(query, p);
            }
        }
    }
}
