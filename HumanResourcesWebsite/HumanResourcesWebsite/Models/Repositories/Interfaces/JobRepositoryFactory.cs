using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HumanResourcesWebsite.Models.Repositories.Real;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public class JobRepositoryFactory
    {
        public static IJobRepository GetJobRepository()
        {
            var mode = WebConfigurationManager.AppSettings["Mode"];
            switch (mode)
            {
                case "Test":
                    return new MockJobRepository();
                default:
                    return new JobRepository();
            }
        }
    }
}