using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Web.ViewModels
{
    public class PostsAndCategoriesViewModel
    {
        public List<Post> Posts { get; set; }
        public int SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}