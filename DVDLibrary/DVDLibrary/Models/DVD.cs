using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace DVDLibrary.Models
{
    public class DVD
    {
        public int ID { get; set; }
        [DisplayName("Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        [DisplayName("MPAA Rating")]
        public MPAARating MPAARating { get; set; }
        public string Studio { get; set; }
        [DisplayName("Andrew's Rating")]
        public int UserRating { get; set; }
        public string URL { get; set; }
    }
}