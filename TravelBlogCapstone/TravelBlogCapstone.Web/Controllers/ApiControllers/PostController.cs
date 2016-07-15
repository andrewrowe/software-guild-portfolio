using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;

namespace TravelBlogCapstone.Web.Controllers.ApiControllers
{
    public class PostController : ApiController
    {
        AdminManager manager = new AdminManager();
        HomeManager homeManager = new HomeManager();

        [Authorize(Roles = "Admin")]
        [ActionName("GetPendingPosts")]
        public List<Post> GetPendingPosts()
        {
            return manager.GetAllPendingPosts();
        }

        [Authorize(Roles = "Admin")]
        [ActionName("GetAllPosts")]
        public List<Post> GetAllPosts()
        {
            return manager.GetAllPosts();
        }

        [ActionName("GetOnlinePost")]
        public Post GetOnlinePost(int id)
        {
            return homeManager.GetPost(id);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("ApprovePost")]
        [HttpPut]
        public void ApprovePost(int id)
        {
           manager.ApprovePost(id);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("AddRemark")]
        [HttpPut]
        public void AddRemark(AddRemarkVM model)
        {
            var post = new Post();
            post.Id = model.Id;
            post.Remark = model.Remark;
            
            manager.DisaprovedPost(post);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("AddCategory")]
        [HttpPut]
        public void AddCategory(string id)
        {
            if (!manager.CategoryExists(id))
            {
                manager.AddCategory(id);
            }
        }

        [Authorize(Roles = "Admin")]
        [ActionName("CheckCategory")]
        [HttpGet]
        public bool CheckCategory(string category)
        {
            return !manager.CategoryExists(category);
        }

        [Authorize(Roles = "Admin,Employee")]
        [ActionName("GetTags")]
        [HttpGet]
        public List<string> GetTags()
        {
            return manager.GetTags().Select(t=>t.TagName).ToList();
        }

        [Authorize(Roles = "Admin,Employee")]
        [ActionName("DeletePost")]
        [HttpDelete]
        public HttpResponseMessage DeletePost(int id)
        {
            try
            {
                manager.DeletePost(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
            }
            
        }
    }
}
