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
    public class AddOrderWorkflow : IWorkflow
    {
        public void Execute(DateTime date)
        {
            OrderManager manager = new OrderManager();
            DateTime orderDate = date;

            string firstName = UserPrompts.GetStringFromUser("Enter customer's first name: ");
            string lastName = UserPrompts.GetStringFromUser("Enter customer's last name: ");
            string state = UserPrompts.GetStateFromUser("Enter state abbreviation (i.e. PA): ");
            string productType = UserPrompts.GetProductTypeFromUser("Enter product type: ");
            decimal area = UserPrompts.GetDecimalFromUser("Enter total area: ");

            Response<Order> orderResponse = manager.CreateOrder(firstName, lastName, productType, area, orderDate, state);
            Console.WriteLine(orderResponse.Message);

            if (orderResponse.Success)
            {
                Console.Clear();
                OrderScreens.PrintOrder(orderResponse.Data);
                bool confirm = UserPrompts.AskForConfirmation("Would you like to save this order?");

                if (confirm)
                {
                    var saveResponse = manager.SaveOrder(orderResponse.Data);
                    Console.WriteLine(saveResponse.Message);
                }
            }

            UserPrompts.PressKeyForMainMenu();
        }
    }
}
