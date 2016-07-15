using System;
using System.Collections.Generic;
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
    public class HomeManager
    {
        private readonly IPostsRepository _postRepo;
        private readonly ICategoriesRepository _categoryRepo;

        public HomeManager()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _postRepo = kernel.Get<IPostsRepository>();
            _categoryRepo = kernel.Get<ICategoriesRepository>();
        }

        public List<Post> Get3LastPostsOnline()
        {
            var posts = _postRepo.GetAll().OrderByDescending(p => p.LastModifiedDate).Where(p => p.StatusId == 1 && (p.PublishedDate <= DateTime.Today) && (p.ExpiredDate == null || p.ExpiredDate > DateTime.Today)).Take(3).ToList();
            return posts;
        }

        public List<Post> GetAllPostsOnline()
        {
            var posts = _postRepo.GetAll().OrderByDescending(p => p.LastModifiedDate).Where(p => p.StatusId == 1 && (p.PublishedDate <= DateTime.Today) && (p.ExpiredDate == null || p.ExpiredDate > DateTime.Today)).ToList();
            return posts;
        }

        public Post GetPost(int postId)
        {
            var post = _postRepo.Get(postId);
            return post;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _categoryRepo.GetAll();
            return categories;
        }
    }
}
