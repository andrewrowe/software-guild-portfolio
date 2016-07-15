using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanResourcesWebsite.Models.Data;

namespace HumanResourcesWebsite.Models.ViewModels
{
    public class PolicyVM
    {
        public Policy Policy { get; set; }
        public List<SelectListItem> PolicyItems { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public PolicyVM()
        {
            Policy = new Policy();
            PolicyItems = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
        }

        public void SetPolicyItems(IEnumerable<Policy> policies)
        {
            foreach (var policy in policies)
            {
                PolicyItems.Add(new SelectListItem()
                {
                    Value = policy.Id.ToString(),
                    Text = policy.Title
                });
            }
        }

        public void SetCategories(IEnumerable<Policy> categories)
        {
            foreach (var category in categories)
            {
                //Categories.Distinct(category.Category).Add(new SelectListItem()
                //{
                //    Value = category.Category,
                //    Text = category.Category
                //});
            }
        }
    }
}