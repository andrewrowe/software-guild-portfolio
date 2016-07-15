using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models
{
    public class Education : IValidatableObject
    {
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        [Display(Name = "Date Started"), DataType(DataType.Date)]
        public DateTime DateStarted { get; set; }
        [Display(Name = "Date Graduated"), DataType(DataType.Date)]
        public DateTime DateCompleted { get; set; }
        public Address Address { get; set; }
        [Display(Name = "Degree Earned (if applicable)")]
        public string DegreeEarned { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (SchoolName.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter the school name"));
            }
            if (DateStarted > DateTime.Now)
            {
                errors.Add(new ValidationResult("Please enter a valid start date"));
            }
            if (DegreeEarned.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter the degree you earned or 'NA' if it doesn't apply"));
            }

            return errors;
        }
    }
}