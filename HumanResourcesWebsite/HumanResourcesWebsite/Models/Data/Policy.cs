using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanResourcesWebsite.Models.Data
{
    public class Policy
    {
        public int Id { get; set; }
        public string PolicyText { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }
}