using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Data.DapperRepositories;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Web.Controllers.ApiControllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class EmployeeController : ApiController
    {
        EmployeeManager employeeMan = new EmployeeManager();

        [ActionName("GetCategories")]
        public List<Category> Get()
        {
            return employeeMan.GetAllCategories();
        }

        [ActionName("GetUserPostsIndex")]
        public List<Post> GetUserPostsIndex()
        {
            return employeeMan.GetAllUserPostsIndex(User.Identity.GetUserId());
        }

        [ActionName("GetApprovedPosts")]
        public List<Post> GetApprovedPosts()
        {
            return employeeMan.GetAllApprovedPost().Where(p => p.UserId == User.Identity.GetUserId()).ToList();
        }

    
        [ActionName("RemovePost")]
        [HttpPut]
        public HttpResponseMessage RemovePost(string id)
        {
            try
            {
                employeeMan.DeletePost(int.Parse(id));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }
        }

        public HttpResponseMessage Post(Post newPost)
        {
            try
            {
                employeeMan.CreateNewPost(newPost);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
