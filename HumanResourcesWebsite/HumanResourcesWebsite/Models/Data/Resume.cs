using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HumanResourcesWebsite.Models
{
    public class Resume
    {
        public int Id { get; set; }
        [Display(Name = "Prior Education")]
        public List<Education> PreviousEducation { get; set; }
        [Display(Name = "Prior Employment")]
        public List<WorkHistory> PreviousJobs { get; set; }
    }
}