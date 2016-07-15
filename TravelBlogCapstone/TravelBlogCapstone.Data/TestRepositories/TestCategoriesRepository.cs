using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBlogCapstone.Data.IRepository;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Data.TestRepositories
{
    public class TestCategoriesRepository : ICategoriesRepository
    {
        private static List<Category> _categories = new List<Category>
        {
            new Category {CategoryName = "Hotel", Id = 1},
            new Category {CategoryName = "Restaurant", Id = 2},
            new Category {CategoryName = "Trip in Europe", Id = 3},
            new Category {CategoryName = "Trip in US", Id = 4},
            new Category {CategoryName = "Trip in Asia", Id = 5},
            new Category {CategoryName = "Organized Trip", Id = 6}
        };

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category Get(int categoryid)
        {
            return _categories.FirstOrDefault(m => m.Id == categoryid);
        }

        public Category Insert(Category category)
        {
            _categories.Add(category);
            return category;
        }

        public void Update(Category category)
        {
            var updateCategory = _categories.FirstOrDefault(m => m.Id == category.Id);
            updateCategory.CategoryName = category.CategoryName;
        }

        public void Delete(int categoryid)
        {
            var removeCategory = _categories.FirstOrDefault(m => m.Id == categoryid);
            _categories.Remove(removeCategory);
        }
    }
}
