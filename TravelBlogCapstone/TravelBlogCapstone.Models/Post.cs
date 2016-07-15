using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TravelBlogCapstone.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int? UpdatedPostId { get; set; }
        public string Title { get; set; }
        public string PostContent { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime InitialPostDate { get; set; }
        public DateTime? UpdatedPostDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiredDate { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public Status Status {get{return (Status) StatusId;} set{StatusId = (int) value;}}
        public string StatusString { get { return Status.ToString(); } }
        public List<int> CategoriesId { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> TagsName { get; set; }
        public List<Tag> Tags { get; set; }
        public string Remark { get; set; }
        public Post UpdatedPost { get; set; }
    }
}
