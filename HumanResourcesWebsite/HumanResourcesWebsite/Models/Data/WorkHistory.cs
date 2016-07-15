using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models
{
    public class WorkHistory : IValidatableObject
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Date Started"), DataType(DataType.Date)]
        public DateTime DateStarted { get; set; }
        [Display(Name = "Date Left"), DataType(DataType.Date)]
        public DateTime DateLeft { get; set; }
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }
        [Display(Name = "Reason for Leaving")]
        public string ReasonLeft { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Can We Contact?")]
        public YesOrNo CanWeContact { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (CompanyName.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter the company name"));
            }
            if (DateStarted > DateTime.Now)
            {
                errors.Add(new ValidationResult("Please enter a valid start date"));
            }
            if (JobTitle.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a job title"));
            }
            if (JobDescription.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a job description"));
            }
            if (Phone.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a phone number"));
            }
            if (ReasonLeft.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter the reason you left"));
            }

            return errors;
        }
    }
}