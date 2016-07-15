using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Data.DapperRepositories;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.ViewModels;

namespace TravelBlogCapstone.Web.Controllers
{
    public class HomeController : ApplicationBaseController
    {
        HomeManager manager = new HomeManager();

        public ActionResult Index()
        {
            var model = manager.Get3LastPostsOnline();
            return View(model);
        }

        public ActionResult Posts()
        {
            var model = new PostsAndCategoriesViewModel();
            model.Posts = manager.GetAllPostsOnline();
            model.Categories = manager.GetAllCategories();
            return View(model);
        }

        public ActionResult Post(int id)
        {
            var model = manager.GetPost(id);
            return View(model);
        }

        public ActionResult GetStaticPage(int id)
        {
            StaticPage selectedPage = new StaticPage();
            foreach (StaticPage page in DapperPagesRepository.GetAll())
            {
                if (page.ID == id)
                {
                    selectedPage = page;
                }
            }
            return View(selectedPage);
        }
    }
}