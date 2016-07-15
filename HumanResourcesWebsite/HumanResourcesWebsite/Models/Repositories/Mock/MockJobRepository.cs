using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Repositories.Interfaces;

namespace HumanResourcesWebsite.Models.Repositories
{
    public class MockJobRepository : IJobRepository
    {
        private static List<Job> _jobs = new List<Job>()
        {
            new Job()
            {
                Title = "Field Marketing Representative",
                Description = "Best Entry Level Sales position available - and we can prove it. " +
                              "We have 2 open positions due to recent promotions of our staff! " +
                              "Education required: None * Skills required: Strong communication and persuasive personality",
                JobStatus = JobStatus.Filled,
                Id = 1
            },
            new Job()
            {
                Title = "Receptionist/Administrative Assistant",
                Description = "Currently looking for an experienced Receptionist to fill a " +
                              "vacant position. The ideal candidate will have at least 2 years of prior experience " +
                              "working as a Receptionist.",
                JobStatus = JobStatus.Open,
                Id = 2
            },
            new Job()
            {
                Title = "Office Assistant/Scheduler",
                Description = "The Office assistant/Scheduler will be responsible for scheduling and " +
                              "helping with the administrative and clerical needs of the office. This includes managing SGCorp's client's and " +
                              "schedules, and helping to ensure that the entire office is well organized and prepared to carry out its daily tasks.",
                JobStatus = JobStatus.Open,
                Id = 3
            },
            new Job()
            {
                Title = "Front Desk Associate",
                Description =
                    "The Front Desk Associate will greet and direct members, guests and staff as they enter " +
                    "SGCorp and provide control of the front door location.  This position will provide assistance to employees' requests along with inquiries " +
                    "about SGCorp operations and policies as well as perform various administrative duties as directed by the General Manager or Operations Supervisor.",
                JobStatus = JobStatus.Open,
                Id = 4
            }
        };

        public List<Job> GetAll()
        {
            return _jobs;
        }

        public Job Get(int id)
        {
            return _jobs.First(m => m.Id == id);
        }

        public void Add(Job job)
        {
            job.Id = _jobs.Max(m => m.Id) + 1;
            job.JobStatus = JobStatus.Open;
            _jobs.Add(job);
        }

        public void Remove(int id)
        {
            _jobs.RemoveAll(m => m.Id == id);
        }

        public void Edit(Job job)
        {
            var selectedJob = _jobs.First(m => m.Id == job.Id);

            selectedJob.Title = job.Title;
            selectedJob.Description = job.Description;
            selectedJob.JobStatus = job.JobStatus;
        }
    }
}