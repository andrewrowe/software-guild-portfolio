using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryBLL;
using FlooringMasteryModels;
using Microsoft.SqlServer.Server;

namespace FlooringMasteryUI.Utilities
{
    public class OrderScreens
    {
        public static void PrintOrder(Order order)
        {
            OrderManager manager = new OrderManager();
            if (order.Status == OrderStatus.Active)
            {
                Console.WriteLine("\nOrder Information");
                Console.WriteLine("========================");
                Console.WriteLine($"Order ID: {order.OrderID}");
                Console.WriteLine($"Customer Name: {order.FirstName} {order.LastName}");
                Console.WriteLine($"State: {order.State}");

                TaxRate taxRate = manager.GetTaxRate(order.State);

                Console.WriteLine($"Area: {order.Area} sq. feet");
                Console.WriteLine($"\nProduct Type: {order.ProductType}");

                Product product = manager.GetProduct(order.ProductType);

                Console.WriteLine($"Cost per square foot: {product.CostPerSquareFoot:c}");
                Console.WriteLine($"Labor cost per square foot: {product.LaborPerSquareFoot:c}");
                Console.WriteLine($"Material cost: {order.TotalMaterialCost:c}");
                Console.WriteLine($"Labor cost: {order.TotalLaborCost:c}");
                Console.WriteLine($"\nTax rate: {taxRate.Percent} %");
                Console.WriteLine($"Tax: {order.TotalTax:c}");
                Console.WriteLine($"Total: {order.TotalCost:c}");
                Console.WriteLine("========================");
            }
        }

        public static void WorkflowErrorScreen(string message)
        {
            Console.WriteLine("An error occurred.\n{0}", message);
            UserPrompts.PressKeyToContinue();
        }
    }
}
