using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanResourcesWebsite.Models;
using HumanResourcesWebsite.Models.Repositories.Interfaces;
using HumanResourcesWebsite.Models.ViewModels;

namespace HumanResourcesWebsite.Controllers
{
    public class AdminController : Controller
    {
        private IJobRepository _jobRepository = JobRepositoryFactory.GetJobRepository();
        private IApplicantRepository _applicantRepository = ApplicantRepositoryFactory.GetApplicantRepository();

        public ActionResult Jobs()
        {
            var model = _jobRepository.GetAll();
            return View(model);
        }

        public ActionResult AddJob()
        {
            var model = new Job();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddJob(Job job)
        {
            if (ModelState.IsValid)
            {
                _jobRepository.Add(job);
                return RedirectToAction("Jobs");
            }

            return View(job);
        }

        public ActionResult EditJob(int id)
        {
            var model = _jobRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(Job job)
        {
            if (ModelState.IsValid)
            {
                _jobRepository.Edit(job);
                return RedirectToAction("Jobs");
            }

            return View(job);
        }

        public ActionResult DeleteJob(int id)
        {
            var model = _jobRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteJob(Job job)
        {
            _jobRepository.Remove(job.Id);
            return RedirectToAction("Jobs");
        }

        public ActionResult Applicants()
        {
            var model = _applicantRepository.GetAll();
            return View(model);
        }

        public ActionResult EditApplicant(int id)
        {

            var viewModel = new ApplicationVM();
            viewModel.Applicant = _applicantRepository.Get(id);
            viewModel.SetJobItems(_jobRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditApplicant(ApplicationVM applicationVM)
        {
            if (ModelState.IsValid)
            {
                applicationVM.Applicant.JobApplied = new List<Job>();

                foreach (var id in applicationVM.SelectedJobs)
                    applicationVM.Applicant.JobApplied.Add(_jobRepository.Get(id));
                _applicantRepository.Edit(applicationVM.Applicant);
                return RedirectToAction("Applicants");
            }

            return View(applicationVM);
        }

        public ActionResult DeleteApplicant(int id)
        {
            var model = _applicantRepository.Get(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteApplicant(Applicant applicant)
        {
            _applicantRepository.Remove(applicant.Id);
            return RedirectToAction("Applicants");
        }

        public ActionResult Resumes()
        {
            var model = _applicantRepository.GetAll();
            return View(model);
        }

        public ActionResult SelectResume(int id)
        {
            var model = _applicantRepository.Get(id);
            return View(model);
        }

        public ActionResult SelectApplicant(int id)
        {
            var model = _applicantRepository.Get(id);
            return View(model);
        }
    }
}