using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryBLL;
using FlooringMasteryModels;
using FlooringMasteryUI.Interfaces;
using FlooringMasteryUI.Utilities;

namespace FlooringMasteryUI.Workflows
{
    public class DisplayOrdersWorkflow : IWorkflow
    {
        public void Execute(DateTime date)
        {
            Console.Clear();
            ShowOrders(date);
            SubMenu.Execute(date);
        }

        public void ShowOrders(DateTime date)
        {
            OrderManager manager = new OrderManager();
            List<Order> orders = manager.GetAllOrders(date);

            if (orders.Count == 0)
                Console.WriteLine("There are no orders for that date.");

            foreach (var order in orders)
            {
                OrderScreens.PrintOrder(order);
            }
        }
    }
}
