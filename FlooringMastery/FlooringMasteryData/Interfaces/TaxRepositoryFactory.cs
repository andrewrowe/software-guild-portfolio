using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.FakeRepos;
using FlooringMasteryData.RealRepos;

namespace FlooringMasteryData.Interfaces
{
    public class TaxRepositoryFactory
    {
        public static ITaxRepository GetTaxRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new FakeTaxRepository();
                default:
                    return new TaxRepository();
            }
        }
    }
}
