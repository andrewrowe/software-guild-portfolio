using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibrary.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        public int DVD_ID { get; set; }
        public int BorrowerID { get; set; }
        public string BorrowerName { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string BorrowerNotes { get; set; }
        public int? BorrowerRating { get; set; }
    }
}