using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;

namespace TravelBlogCapstone.Web.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class EmployeeController : ApplicationBaseController
    {
        HomeManager manager = new HomeManager();
        EmployeeManager eManager = new EmployeeManager();

        // GET: Employee

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult CreatePost()
        {

            Post newPost = new Post();
            newPost.UserId = User.Identity.GetUserId();
            return View(newPost);
        }

        public ActionResult UpdatePost(int id)
        {
            var post = manager.GetPost(id);
            var model = new EditPostVM();
            model.Post = post;
            

            var categories = eManager.GetAllCategories();
            model.SetCategoryList(categories, post.CategoriesId);
            model.SetStatusList();

            return View(model);
        }

        public ActionResult EmployeeViewPosts()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdatePost(EditPostVM model)
        {

            if (ModelState.IsValid)
            {
                //comma separated list of tags
                var tags = Request["tagsArray"];

                var post = new Post();

                post = model.Post;
                post.TagsName = tags.Split(',').ToList();
                post.CategoriesId = model.CategoryIds;
                


                eManager.UpdatePost(post, model.StatusId);
                //call manager

                return RedirectToAction("Index");
            }

            var postInfo = manager.GetPost(model.Post.Id);
            model.Post = postInfo;

            var categories = eManager.GetAllCategories();
            model.SetCategoryList(categories, postInfo.CategoriesId);
            model.SetStatusList();

            return View(model);
        }

    }
}