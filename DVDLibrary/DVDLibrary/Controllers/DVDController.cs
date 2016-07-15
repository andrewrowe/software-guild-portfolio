using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using DVDLibrary.Models;
using DVDLibrary.Repositories;
using DVDLibrary.ViewModels;

namespace DVDLibrary.Controllers
{
    public class DVDController : ApiController
    {
        public List<DVD> Get()
        {
            DVDRepository dvdRepository = new DVDRepository();
            return dvdRepository.GetAllDVDs();
        }

        //public HttpResponseMessage Delete(string title)
        //{
        //    DVDRepository dvdRepository = new DVDRepository();
        //    dvdRepository.RemoveDVD(title);

        //    var response = Request.CreateResponse(HttpStatusCode.OK);

        //    string uri = Url.Link("DefaultApi", new {title});
        //    response.Headers.Location = new Uri(uri);

        //    return response;
        //}
    }
}
