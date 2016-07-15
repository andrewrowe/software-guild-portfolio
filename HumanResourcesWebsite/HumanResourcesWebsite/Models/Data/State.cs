using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models.Data
{
    public class State : IValidatableObject
    {
        [Display(Name = "State")]
        public string StateAbbreviation { get; set; }
        [Display(Name = "State Name")]
        public string StateName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (StateAbbreviation.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a state abbreviation"));
            }
            if (StateName.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter a state name"));
            }

            return errors;
        }
    }
}