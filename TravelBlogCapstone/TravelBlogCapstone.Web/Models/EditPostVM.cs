using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelBlogCapstone.Models;

namespace TravelBlogCapstone.Web.Models
{
    public class EditPostVM
    {
        public Post Post { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int StatusId { get; set; }
        public List<SelectListItem> StatusList { get; set; }


        public EditPostVM()
        {
            Categories = new List<SelectListItem>();
            StatusList = new List<SelectListItem>();
        }

        public void SetCategoryList(IEnumerable<Category> categories, List<int> ids)
        {
            foreach (var category in categories)
            {
                if (ids.Contains(category.Id))
                {
                    Categories.Add(new SelectListItem()
                    {
                        Value = category.Id.ToString(),
                        Text = category.CategoryName,
                        Selected = true
                    });

                    continue;
                }

                Categories.Add(new SelectListItem()
                {
                    Value = category.Id.ToString(),
                    Text = category.CategoryName,
                    Selected = false
                });
            }
        }

        public void SetStatusList()
        {

            StatusList.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Save Draft",
                Selected = true
            });

            StatusList.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Send for Approval",
                Selected = true
            });
        }


    }
}