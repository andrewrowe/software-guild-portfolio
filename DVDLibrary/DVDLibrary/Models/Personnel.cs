using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace DVDLibrary.Models
{
    public class Personnel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
    }
}