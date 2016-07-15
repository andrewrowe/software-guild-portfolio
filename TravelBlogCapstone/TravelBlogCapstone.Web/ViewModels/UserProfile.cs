using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBlogCapstone.Models;
using TravelBlogCapstone.Web.Models;

namespace TravelBlogCapstone.Web.ViewModels
{
    public class UserProfile
    {
        public int NumberNoReadMessages { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}