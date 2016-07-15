using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace TravelBlogCapstone.Models
{
    public enum Status
    {
        Approved = 1,
        PendingNew = 2,
        PendingUpdate = 3,
        Draft = 4,
        Deleted = 5
    }
}
