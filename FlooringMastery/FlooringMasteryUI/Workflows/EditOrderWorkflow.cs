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
    public class EditOrderWorkflow : IWorkflow
    {
        public void Execute(DateTime date)
        {
            Console.Clear();
            OrderManager manager = new OrderManager();
            DisplayOrdersWorkflow displayWF = new DisplayOrdersWorkflow();
            displayWF.ShowOrders(date);
            bool confirmEdit = false;

            int orderID = UserPrompts.GetOrderNumberFromUser("Please enter the order number: ");

            var order = manager.LoadOrder(date, orderID);
            if (!order.Success)
                Console.WriteLine(order.Message);
            else
            {
                Console.Clear();
                OrderScreens.PrintOrder(order.Data);
                confirmEdit = UserPrompts.AskForConfirmation("Edit this order?");
            }

            if (order.Success && confirmEdit)
            {
                Order newOrder = EditOrder(order.Data);

                Console.Clear();
                OrderScreens.PrintOrder(newOrder);
                bool confirm = UserPrompts.AskForConfirmation("Would you like to save these changes?");

                if (confirm)
                {
                    if (order.Data.OrderDate == newOrder.OrderDate)
                        manager.RemoveOrder(order.Data);
                    else
                        manager.DeleteOrder(order.Data);


                    var saveResponse = manager.SaveOrder(newOrder);
                    Console.WriteLine(saveResponse.Message);
                }
            }

            UserPrompts.PressKeyForMainMenu();
        }

        private static Order EditOrder(Order order)
        {
            OrderManager manager = new OrderManager();
            Order newOrder = new Order();
         
            newOrder.FirstName = UserPrompts.EditStringByUser($"Enter customer's first name ({order.FirstName}): ", order.FirstName);
            newOrder.LastName = UserPrompts.EditStringByUser($"Enter customer's last name ({order.LastName}): ", order.LastName);
            newOrder.State = UserPrompts.EditStateByUser($"Enter state abbreviation ({order.State}): ", order.State);
            newOrder.OrderDate = UserPrompts.EditDateByUser($"Enter order date ({order.OrderDate}): ", order.OrderDate);
            newOrder.ProductType = UserPrompts.EditProductTypeByUser($"Enter product type ({order.ProductType}): ", order.ProductType);
            newOrder.Area = UserPrompts.EditDecimalByUser($"Enter total area ({order.Area}): ", order.Area);

            if (order.OrderDate == newOrder.OrderDate)
                newOrder.OrderID = order.OrderID;
            else
                newOrder.OrderID = manager.UpdateOrderID(newOrder).Data.OrderID;

            newOrder.TotalLaborCost = manager.CalculateLaborCost(newOrder.Area,
                newOrder.ProductType);
            newOrder.TotalMaterialCost = manager.CalculateMaterialCost(newOrder.Area,
                newOrder.ProductType);
            newOrder.TotalTax = manager.CalculateTax(newOrder.TotalLaborCost,
                newOrder.TotalMaterialCost, newOrder.State);
            newOrder.TotalCost = manager.CalculateTotal(newOrder.TotalLaborCost,
                newOrder.TotalMaterialCost, newOrder.TotalTax);
            newOrder.Status = OrderStatus.Active;

            return newOrder;
        }
    }
}
