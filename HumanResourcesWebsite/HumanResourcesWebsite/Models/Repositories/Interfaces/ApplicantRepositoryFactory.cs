using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HumanResourcesWebsite.Models.Repositories.Real;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public class ApplicantRepositoryFactory
    {
        public static IApplicantRepository GetApplicantRepository()
        {
            var mode = WebConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new MockApplicantRepository();
                default:
                    return new ApplicantRepository();
            }
        }
    }
}