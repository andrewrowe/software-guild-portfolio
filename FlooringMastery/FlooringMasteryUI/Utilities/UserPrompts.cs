using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryBLL;
using FlooringMasteryModels;
using FlooringMasteryUI.Workflows;

namespace FlooringMasteryUI.Utilities
{
    public class UserPrompts
    {
        public static int GetChoiceFromUser(string message, int maxChoice)
        {
            do
            {
                int choice;
                Console.Write(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice) && choice > 0 && choice < maxChoice + 1)
                    return choice;

                OrderScreens.WorkflowErrorScreen("Please enter a valid choice.");
            } while (true);

        }

        public static string GetProductTypeFromUser(string message)
        {
            do
            {
                Console.Clear();
                OrderManager manager = new OrderManager();

                Console.WriteLine(message);
                string productType = Console.ReadLine();

                foreach (var product in manager.products)
                {
                    if (product.ProductType == productType)
                        return productType;
                }

                OrderScreens.WorkflowErrorScreen("Please enter a valid product type (make sure first letter is capitalized).");
            } while (true);
        }

        public static string GetStateFromUser(string message)
        {
            do
            {
                Console.Clear();
                OrderManager manager = new OrderManager();

                Console.WriteLine(message);
                string state = Console.ReadLine().ToUpper();

                foreach (var s in manager.taxRates)
                {
                    if (s.StateAbbreviation == state)
                        return state;
                }
                
                OrderScreens.WorkflowErrorScreen("Please enter a valid state abbreviation. Should be two letters.");
            } while (true);
        }

        public static DateTime GetDateFromUser(string message)
        {
            do
            {
                Console.Clear();
                DateTime result;
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out result))
                    return result;

                OrderScreens.WorkflowErrorScreen("Please enter a valid date.");
            } while (true);
        }

        public static string GetStringFromUser(string message)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (input.Length > 0)
                    return input;

                OrderScreens.WorkflowErrorScreen("You must enter something.");
            } while (true);
        }

        public static int GetOrderNumberFromUser(string message)
        {
            do
            {
                int result;
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result > 0)
                    return result;

                OrderScreens.WorkflowErrorScreen("Please enter a valid order number.");
            } while (true);
        }

        public static decimal GetDecimalFromUser(string message)
        {
            do
            {
                Console.Clear();
                decimal result;
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out result) && result > 0)
                    return result;

                OrderScreens.WorkflowErrorScreen("Please enter a valid decimal.");
            } while (true);
        }

        public static string EditStringByUser(string message, string property)
        {
            Console.Clear();
            string input = "";
            Console.WriteLine(message);
            input = Console.ReadLine();

            if (input == "")
                return property;

            return input;
        }

        public static string EditProductTypeByUser(string message, string productType)
        {
            do
            {
                Console.Clear();
                OrderManager manager = new OrderManager();

                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (input.Length == 0)
                    return productType;

                foreach (var product in manager.products)
                {
                    if (input == product.ProductType)
                        return input;
                }

                OrderScreens.WorkflowErrorScreen("Please enter a valid product type (make sure first letter is capitalized).");
            } while (true);
        }

        public static string EditStateByUser(string message, string state)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(message);
                OrderManager manager = new OrderManager();
                string input = Console.ReadLine().ToUpper();

                if (input.Length == 0)
                    return state;

                foreach (var s in manager.taxRates)
                {
                    if (s.StateAbbreviation == input)
                        return input;
                }

                OrderScreens.WorkflowErrorScreen("Please enter a valid state abbreviation. Should be two letters.");
            } while (true);
        }

        public static DateTime EditDateByUser(string message, DateTime property)
        {
            do
            {
                Console.Clear();
                string input = "";
                DateTime result;
                Console.WriteLine(message);
                input = Console.ReadLine();

                if (input == "")
                    return property;

                if (DateTime.TryParse(input, out result))
                    return result;
            } while (true);
        }

        public static decimal EditDecimalByUser(string message, decimal property)
        {
            do
            {
                Console.Clear();
                string input = "";
                decimal result;
                Console.WriteLine(message);
                input = Console.ReadLine();

                if (input == "")
                    return property;

                if (decimal.TryParse(input, out result))
                    return result;
            } while (true);
        }

        public static bool AskForConfirmation(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Y/N");
            string answer = Console.ReadLine().ToUpper();

            if (answer == "Y")
            {
                Console.Clear();
                return true;
            }
            else
            {
                Console.Clear();
                return false;
            }
        }

        public static void PressKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void PressKeyForMainMenu()
        {
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
            MainMenu.Execute();
        }
    }
}
