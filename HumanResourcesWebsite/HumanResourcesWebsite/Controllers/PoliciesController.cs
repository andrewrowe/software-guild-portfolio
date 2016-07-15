using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanResourcesWebsite.Models.Repositories.Mock;

namespace HumanResourcesWebsite.Controllers
{
    public class PoliciesController : Controller
    {
        MockPolicyRepository policyRepository = new MockPolicyRepository();

        public ActionResult ViewPolicies()
        {
            var model = policyRepository.GetAll();
            return View(model);
        }

        public ActionResult ManagePolicies()
        {
            var model = policyRepository.GetAll();
            return View(model);
        }

        public ActionResult ManageCategories()
        {
            var model = policyRepository.GetAll();
            return View(model);
        }
    }
}