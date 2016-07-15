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
    public class AdminManager
    {
        private readonly IPostsRepository _postRepo;
        private readonly ITagsRepository _tagsRepo;
        private readonly ICategoriesRepository _categoryRepo;

        public AdminManager()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _postRepo = kernel.Get<IPostsRepository>();
            _categoryRepo = kernel.Get<ICategoriesRepository>();
            _tagsRepo = kernel.Get<ITagsRepository>();
        }

        public List<Post> GetAllPendingPosts()
        {
            var posts = _postRepo.GetAll().Where(p => (p.StatusId == 2 || p.StatusId == 3)).ToList();
            return posts;
        }

        public List<Post> GetAllPosts()
        {
            var posts = _postRepo.GetAll().ToList();
            return posts;
        }

        public void DisaprovedPost(Post post)
        {
            post.Status = Status.Draft;
            _postRepo.DisapprovePost(post);
        }

        public void ApprovePost(int postId)
        {
            _postRepo.ApprovePost(postId);
        }

        public List<Tag> GetTags()
        {
            return _tagsRepo.GetAll();
        }

        public bool CategoryExists(string str)
        {
            var categories = _categoryRepo.GetAll().Select(c=>c.CategoryName.ToUpper());

            if (categories.Contains(str.ToUpper()))
            {
                return true;
            }
            return false;
        }

        public void AddCategory(string category)
        {
            var Category = new Category() {CategoryName = category};
            
            _categoryRepo.Insert(Category);
        }

        public void AddPage(StaticPage newPage)
        {
            DapperPagesRepository.Add(newPage);
        }

        public void DeletePost(int id)
        {
            _postRepo.Delete(id);
        }
    }
}
