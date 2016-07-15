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
    public class JobsController : Controller
    {
        private IJobRepository _jobRepository = JobRepositoryFactory.GetJobRepository();
        private IApplicantRepository _applicantRepository = ApplicantRepositoryFactory.GetApplicantRepository();
        private IStateRepository _stateRepository = StateRepositoryFactory.GetStateRepository();

        public ActionResult JobListings()
        {
            var model = _jobRepository.GetAll();
            return View(model);
        }

        public ActionResult Apply(int id)
        {
            var viewModel = new ApplicationVM();
            viewModel.Job = _jobRepository.Get(id);
            viewModel.SetJobItems(_jobRepository.GetAll());
            viewModel.SetStateItems(_stateRepository.GetAll());
            viewModel.Applicant = new Applicant();
            viewModel.Education = new Education();
            viewModel.WorkHistory = new WorkHistory();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Apply(ApplicationVM applicationVM)
        {
            if (ModelState.IsValid)
            {
                applicationVM.Applicant.Resume = new Resume();
                applicationVM.Applicant.Resume.PreviousEducation = new List<Education>();
                applicationVM.Applicant.Resume.PreviousJobs = new List<WorkHistory>();
                applicationVM.Applicant.JobApplied = new List<Job>();

                applicationVM.Education.Address.State =
                    _stateRepository.Get(applicationVM.Education.Address.State.StateAbbreviation);
                applicationVM.WorkHistory.Address.State =
                    _stateRepository.Get(applicationVM.WorkHistory.Address.State.StateAbbreviation);
                applicationVM.Applicant.Resume.PreviousEducation.Add(applicationVM.Education);
                applicationVM.Applicant.Resume.PreviousJobs.Add(applicationVM.WorkHistory);
                applicationVM.Applicant.JobApplied.Add(_jobRepository.Get(applicationVM.Job.Id));

                _applicantRepository.Add(applicationVM.Applicant);
                ApplyConfirm(applicationVM.Applicant);
                return RedirectToAction("ApplyConfirm");
            }

            return View(applicationVM);
        }

        public ActionResult ApplyConfirm(Applicant applicant)
        {
            return View("ApplyConfirm");
        }
    }
}