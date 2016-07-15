using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using HumanResourcesWebsite.Models.Repositories.Real;

namespace HumanResourcesWebsite.Models.Repositories.Interfaces
{
    public class StateRepositoryFactory
    {
        public static IStateRepository GetStateRepository()
        {
            var mode = WebConfigurationManager.AppSettings["Mode"];
            switch (mode)
            {
                case "Test":
                    return new MockStateRepository();
                default:
                    return new StateRepository();
            }
        }
    }
}