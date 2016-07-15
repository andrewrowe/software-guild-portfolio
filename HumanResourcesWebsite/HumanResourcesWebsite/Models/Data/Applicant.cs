using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models
{
    public class Applicant : IValidatableObject
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Date of Application")]
        public DateTime DateOfApplication { get; set; }
        public Resume Resume { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Job(s) Wanted")]
        public List<Job> JobApplied { get; set; }
        [Display(Name = "Application Status")]
        public ApplicantStatus ApplicantStatus { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (FirstName.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your first name"));
            }
            if (LastName.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your last name"));
            }
            if (DateOfBirth > DateTime.Now)
            {
                errors.Add(new ValidationResult("Please enter a valid birth date"));
            }
            if (Phone.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your phone number"));
            }
            if (Email.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your email"));
            }

            return errors;
        }
    }
}