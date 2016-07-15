using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryBLL;
using FlooringMasteryUI.Interfaces;
using FlooringMasteryUI.Utilities;

namespace FlooringMasteryUI.Workflows
{
    public class RemoveOrderWorkflow : IWorkflow
    {
        public void Execute(DateTime date)
        {
            bool confirm = false;
            OrderManager manager = new OrderManager();
            Console.Clear();
            DisplayOrdersWorkflow displayWF = new DisplayOrdersWorkflow();
            displayWF.ShowOrders(date);

            int orderID = UserPrompts.GetOrderNumberFromUser("Please enter the order number you want to delete\n(you will have a chance to confirm your selection): ");

            var order = manager.LoadOrder(date, orderID);
            Console.WriteLine(order.Message);

            if (order.Success)
            {
                Console.Clear();
                OrderScreens.PrintOrder(order.Data);
                confirm = UserPrompts.AskForConfirmation("Are you sure you want to delete this order?");
            }

            if (confirm)
            {
                var deleteResponse = manager.DeleteOrder(order.Data);
                Console.WriteLine(deleteResponse.Message);
            }

            UserPrompts.PressKeyForMainMenu();
        }
    }
}
