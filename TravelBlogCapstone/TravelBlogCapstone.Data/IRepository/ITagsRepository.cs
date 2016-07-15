using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.IRepository
{
    public interface ITagsRepository
    {
        List<Tag> GetAll();
        Tag Get(string tagName);
        Tag Insert(Tag tag);
        void Update(Tag tag);
        void Delete(string tagName);
    }
}
