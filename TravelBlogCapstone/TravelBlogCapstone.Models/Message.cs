using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBlogCapstone.Models
{
    public class Message
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SendDate { get; set; }
        public string SendUserId { get; set; }
        public string RecipientUserId { get; set; }

        [Required]
        [Display(Name = "Subject:")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Text:")]
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public bool IsSenderDeleted { get; set; }
        public bool IsReceiverDeleted { get; set; }

        [Required]
        [Display(Name = "To:")]
        public string SenderUserName { get; set; }
    }
}
