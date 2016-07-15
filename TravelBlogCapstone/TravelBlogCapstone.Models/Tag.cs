using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBlogCapstone.Models
{
    public class Tag
    {
        public string TagName { get; set; }
        public List<int> PostsId { get; set; }
        public List<Post> Posts { get; set; }
    }
}
