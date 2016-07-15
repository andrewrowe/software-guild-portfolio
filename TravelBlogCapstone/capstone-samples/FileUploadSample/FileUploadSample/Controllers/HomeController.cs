using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace FileUploadSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path =
                        "C:\\Users\\Apprentice\\Documents\\repos\\samples\\capstone-samples\\FileUploadSample\\FileUploadSample\\images\\" + fileName;
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
