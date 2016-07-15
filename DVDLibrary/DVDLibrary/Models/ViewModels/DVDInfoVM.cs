using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DVDLibrary.Models;

namespace DVDLibrary.ViewModels
{
    public class DVDInfoVM
    {
        public DVD DVD { get; set; }
        public List<Personnel> Personnel { get; set; }
        public Personnel Person { get; set; }

        public DVDInfoVM()
        {
            DVD = new DVD();
            Personnel = new List<Personnel>();
            Person = new Personnel();
        }
    }
}