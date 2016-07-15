using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.RealRepos;
using FlooringMasteryModels;

namespace FlooringMasteryData.Interfaces
{
    public class OrderRepositoryFactory
    {
        public static IOrderRepository GetOrderRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new FakeOrderRepository();
                default:
                    return new OrderRepository();
            }
        }
    }
}
