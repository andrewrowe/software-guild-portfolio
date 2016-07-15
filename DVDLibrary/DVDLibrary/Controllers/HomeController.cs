using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DVDLibrary.Models;
using DVDLibrary.Repositories;
using DVDLibrary.ViewModels;
using System.Web.Http;

namespace DVDLibrary.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DVDRepository dvdRepository = new DVDRepository();
            List<DVD> model = dvdRepository.GetAllDVDs();
            return View(model);
        }

        public ActionResult DVDPictures()
        {
            DVDRepository dvdRepository = new DVDRepository();
            List<DVD> model = dvdRepository.GetAllDVDs();
            return View(model);
        }

        public ActionResult DVDInfo(string id)
        {
            DVDRepository dvdRepository = new DVDRepository();
            DVDInfoVM dvdInfoVM = dvdRepository.GetDVDInfo(id);
            return View(dvdInfoVM);
        }

        public ActionResult AddDVD()
        {
            DVDInfoVM dvdInfoVM = new DVDInfoVM();
            return View(dvdInfoVM);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddDVD(DVDInfoVM dvdInfoVM)
        {
            if (ModelState.IsValid)
            {
                int index = 0;

                foreach (var person in dvdInfoVM.Personnel)
                {
                    person.ID = index;
                    index++;
                }
                return View("ConfirmSave", dvdInfoVM);
            }

            return View(dvdInfoVM);
        }

        public ActionResult ConfirmDelete(string id)
        {
            DVDRepository dvdRepository = new DVDRepository();
            DVDInfoVM dvdInfoVM = dvdRepository.GetDVDInfo(id);

            return View(dvdInfoVM);
        }

        public ActionResult DeleteDVD(DVDInfoVM dvdInfoVM)
        {
            DVDInfoVM deletedDvdInfoVm = new DVDInfoVM();
            deletedDvdInfoVm.DVD.Title = dvdInfoVM.DVD.Title;
            DVDRepository dvdRepository = new DVDRepository();
            dvdRepository.RemoveDVD(dvdInfoVM);
            return View(deletedDvdInfoVm);
        }

        public ActionResult SaveDVD(DVDInfoVM dvdInfoVM)
        {
            DVDRepository dvdRepository = new DVDRepository();
            dvdRepository.AddDVD(dvdInfoVM);
            return View(dvdInfoVM);
        }
    }
}