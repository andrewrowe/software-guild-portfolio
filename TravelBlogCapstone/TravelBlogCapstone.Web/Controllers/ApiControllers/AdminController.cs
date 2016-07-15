using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBlogCapstone.BLL;
using TravelBlogCapstone.Data.DapperRepositories;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Web.Controllers.ApiControllers
{
    public class AdminController : ApiController
    {
        private AdminManager adminMan = new AdminManager();

        [Authorize(Roles = "Admin")]
        [ActionName("SavePage")]
        public HttpResponseMessage Post(StaticPage newPage)
        {
            try
            {
                adminMan.AddPage(newPage);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ActionName("GetPages")]
        public List<StaticPage> Get()
        {
            return DapperPagesRepository.GetAll();
        }

        [Authorize(Roles = "Admin")]
        [ActionName("PageDelete")]
        public void Delete(int ID)
        {
            DapperPagesRepository.Delete(ID);
        }

        [Authorize(Roles = "Admin")]
        [ActionName("EditPage")]
        public void Put(StaticPage page)
        {
            DapperPagesRepository.Update(page);
        }
    }
}
