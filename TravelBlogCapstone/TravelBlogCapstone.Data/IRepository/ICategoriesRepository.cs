using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.IRepository
{
    public interface ICategoriesRepository
    {
        List<Category> GetAll();
        Category Get(int categoryid);
        Category Insert(Category category);
        void Update(Category category);
        void Delete(int categoryid);
    }
}
