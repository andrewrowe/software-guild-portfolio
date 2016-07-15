using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HumanResourcesWebsite.Models.Data;
using Microsoft.Ajax.Utilities;

namespace HumanResourcesWebsite.Models
{
    public class Address : IValidatableObject
    {
        public int Id { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress1 { get; set; }
        [Display(Name = "Street Address (cont.)")]
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        [Display(Name = "Zip Code"), DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (StreetAddress1.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your address"));
            }
            if (City.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your city"));
            }
            if (ZipCode.IsNullOrWhiteSpace())
            {
                errors.Add(new ValidationResult("Please enter your zip code"));
            }

            return errors;
        }
    }
}