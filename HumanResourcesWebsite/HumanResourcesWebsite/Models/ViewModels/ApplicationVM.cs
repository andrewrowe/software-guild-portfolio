using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanResourcesWebsite.Models.Data;

namespace HumanResourcesWebsite.Models.ViewModels
{
    public class ApplicationVM
    {
        public Applicant Applicant { get; set; }
        public Job Job { get; set; }
        public List<SelectListItem> StateItems { get; set; }
        public List<SelectListItem> JobItems { get; set; }
        public WorkHistory WorkHistory { get; set; }
        public Education Education { get; set; }
        public List<int> SelectedJobs { get; set; }

        public ApplicationVM()
        {
            Applicant = new Applicant();
            Job = new Job();
            StateItems = new List<SelectListItem>();
            JobItems = new List<SelectListItem>();
            WorkHistory = new WorkHistory();
            Education = new Education();
            SelectedJobs = new List<int>();
        }

        public void SetStateItems(IEnumerable<State> states)
        {
            foreach (var state in states)
            {
                StateItems.Add(new SelectListItem()
                {
                    Value = state.StateAbbreviation,
                    Text = state.StateName
                });
            }
        }

        public void SetJobItems(IEnumerable<Job> jobs)
        {
            foreach (var job in jobs)
            {
                JobItems.Add(new SelectListItem()
                {
                    Value = job.Id.ToString(),
                    Text = job.Title
                });
            }
        }
    }
}