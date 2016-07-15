using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using TravelBlogCapstone.Data.DapperRepositories;

namespace TravelBlogCapstone.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ApplicationBaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        public ActionResult ManageRoles()
        {
            return View();
        }

        public ActionResult CreateStatic()
        {
            return View();
        }

        public ActionResult EditStatic(int id)
        {
            return View(DapperPagesRepository.Get(id));
        }

        public ActionResult ManagePosts()
        {
            return View();
        }
    }
}