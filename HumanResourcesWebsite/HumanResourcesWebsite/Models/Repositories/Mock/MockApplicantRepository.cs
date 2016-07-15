using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using HumanResourcesWebsite.Models.Repositories.Interfaces;

namespace HumanResourcesWebsite.Models.Repositories
{
    public class MockApplicantRepository : IApplicantRepository
    {
        private static List<Applicant> _applicants = new List<Applicant>()
        {
            new Applicant()
            {
                Id = 1,
                DateOfApplication = DateTime.Parse("5/21/16"),
                DateOfBirth = DateTime.Parse("4/18/90"),
                Email = "arowe77@gmail.com",
                FirstName = "Andrew",
                LastName = "Rowe",
                Phone = "717-315-5681",
                ApplicantStatus = ApplicantStatus.Undecided,
                Resume = new Resume()
                {
                    Id = 1,
                    PreviousEducation = new List<Education>()
                    {
                        new Education() {SchoolName = "HACC", DateStarted = DateTime.Parse("9/1/10"),
                            DateCompleted = DateTime.Parse("5/15/14"), DegreeEarned = "BS in Computer Science",
                        Address = new Address()
                        {
                            Id = 1, StreetAddress1 = "123 Something Lane", City = "Harrisburg",
                            State = new State() {StateAbbreviation = "PA", StateName = "Pennsylvania"}, ZipCode = "17011"
                        }}
                    },
                    PreviousJobs = new List<WorkHistory>()
                    {
                        new WorkHistory() {CompanyName = "JC Penney's", DateStarted = DateTime.Parse("1/1/09"),
                            DateLeft = DateTime.Parse("1/1/10"), JobTitle = "Stocker", JobDescription = "Pick things up, put them down",
                            Phone = "555-666-7777", ReasonLeft = "School", Address = new Address() {
                            Id = 1, StreetAddress1 = "4400 Capital City Dr", City = "Camp Hill",
                            State = new State() {StateAbbreviation = "PA", StateName = "Pennsylvania"}, ZipCode = "17043"
                        }, CanWeContact = YesOrNo.Yes},
                        new WorkHistory() {CompanyName = "Bed Bath & Beyond", DateStarted = DateTime.Parse("6/1/14"),
                            DateLeft = DateTime.Parse("5/1/16"), JobTitle = "Front end supervisor",
                            JobDescription = "Ring up items, etc. etc.",
                            Phone = "888-999-0000", ReasonLeft = "Relocated", Address = new Address() {
                            Id = 1, StreetAddress1 = "5000 Arboretum Dr", City = "Mechanicsburg",
                            State = new State() {StateAbbreviation = "PA", StateName = "Pennsylvania"}, ZipCode = "17055"
                        }, CanWeContact = YesOrNo.Yes}
                    }
                },
                JobApplied = new List<Job>()
                {
                    new Job() {Title = "Front Desk Associate",
                    Description =
                        "The Front Desk Associate will greet and direct members, guests and staff as they enter " +
                        "SGCorp and provide control of the front door location.  " +
                        "This position will provide assistance to employees' requests along with inquiries " +
                        "about SGCorp operations and policies as well as perform various administrative duties as " +
                        "directed by the General Manager or Operations Supervisor.",
                    JobStatus = JobStatus.Open}
                }
            },
            new Applicant()
            {
                Id = 2,
                DateOfApplication = DateTime.Parse("5/26/16"),
                DateOfBirth = DateTime.Parse("11/22/75"),
                Email = "jeff22@hotmail.com",
                FirstName = "Jeff",
                LastName = "Goldblum",
                Phone = "555-212-6487",
                ApplicantStatus = ApplicantStatus.Undecided,
                Resume = new Resume()
                {
                    Id = 2,
                    PreviousEducation = new List<Education>()
                    {
                        new Education() {SchoolName = "Berkley", DateStarted = DateTime.Parse("9/1/93"),
                        DateCompleted = DateTime.Parse("5/15/97"), DegreeEarned = "BA in Theatre",
                        Address = new Address()
                        {
                            Id = 1, StreetAddress1 = "432 Another Drive", City = "Akron",
                            State = new State() {StateAbbreviation = "OH", StateName = "Ohio"}, ZipCode = "44311"
                        }},
                        new Education() {SchoolName = "University of Akron", DateStarted = DateTime.Parse("9/1/98"),
                        DateCompleted = DateTime.Parse("5/15/2000"), DegreeEarned = "Masters in Acting Silly",
                        Address = new Address()
                        {
                            Id = 2, StreetAddress1 = "11 Somewhere Rd", City = "Akron",
                            State = new State() {StateAbbreviation = "OH", StateName = "Ohio"}, ZipCode = "44311"
                        }}
                    },
                    PreviousJobs = new List<WorkHistory>()
                    {
                        new WorkHistory() {CompanyName = "Something Else", DateStarted = DateTime.Parse("1/1/09"),
                            DateLeft = DateTime.Parse("1/1/10"), JobTitle = "Stocker", JobDescription = "Pick things up, put them down",
                            Phone = "555-666-7777", ReasonLeft = "School", Address = new Address() {
                            Id = 1, StreetAddress1 = "4400 Capital City Dr", City = "Camp Hill",
                            State = new State() {StateAbbreviation = "PA", StateName = "Pennsylvania"}, ZipCode = "17043"
                        }, CanWeContact = YesOrNo.Yes},
                    }
                },
                JobApplied = new List<Job>()
                {
                    new Job() {Title = "Front Desk Associate",
                    Description =
                        "The Front Desk Associate will greet and direct members, guests and staff as they enter " +
                        "SGCorp and provide control of the front door location.  " +
                        "This position will provide assistance to employees' requests along with inquiries " +
                        "about SGCorp operations and policies as well as perform various administrative duties as " +
                        "directed by the General Manager or Operations Supervisor.",
                    JobStatus = JobStatus.Open},
                    new Job()
                    {Title = "Receptionist/Administrative Assistant",
                    Description = "Currently looking for an experienced Receptionist to fill a " +
                              "vacant position. The ideal candidate will have at least 2 years of prior experience " +
                              "working as a Receptionist.",
                    JobStatus = JobStatus.Open}
                }
            }
        };

        public List<Applicant> GetAll()
        {
            return _applicants;
        }

        public Applicant Get(int id)
        {
            return _applicants.First(m => m.Id == id);
        }

        public void Add(Applicant applicant)
        {
            applicant.Id = _applicants.Max(m => m.Id) + 1;
            applicant.DateOfApplication = DateTime.Now;
            applicant.ApplicantStatus = ApplicantStatus.Undecided;
            _applicants.Add(applicant);
        }

        public void Remove(int id)
        {
            _applicants.RemoveAll(m => m.Id == id);
        }

        public void Edit(Applicant applicant)
        {
            var selectedApplicant = _applicants.First(m => m.Id == applicant.Id);

            selectedApplicant.FirstName = applicant.FirstName;
            selectedApplicant.LastName = applicant.LastName;
            selectedApplicant.DateOfBirth = applicant.DateOfBirth;
            selectedApplicant.DateOfApplication = applicant.DateOfApplication;
            selectedApplicant.Email = applicant.Email;
            selectedApplicant.Phone = applicant.Phone;
            selectedApplicant.ApplicantStatus = applicant.ApplicantStatus;
            selectedApplicant.JobApplied = applicant.JobApplied;
        }
    }
}