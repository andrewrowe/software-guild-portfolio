using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Web.ViewModels
{
    public class MessagesViewModel
    {
        public Message Message { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}