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
    public class ProductRepositoryFactory
    {
        public static IProductRepository GetProductRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new FakeProductRepository();
                default:
                    return new ProductRepository();
            }
        }
    }
}
