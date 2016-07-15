using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models
{
    public class Job : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Job Status")]
        public JobStatus JobStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Title.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a job title", new [] {"Title"}));
            }
            if (Description.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a job description", new [] {"Description"}));
            }

            return errors;
        }
    }
}