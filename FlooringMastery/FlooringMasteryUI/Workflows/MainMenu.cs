using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryUI.Interfaces;
using FlooringMasteryUI.Utilities;
using Microsoft.Win32;

namespace FlooringMasteryUI.Workflows
{
    public class MainMenu
    {
        public static void Execute()
        {
            Console.Clear();
            Console.WriteLine("Andrew's Flooring App");
            Console.WriteLine("=======================");
            Console.WriteLine("\n1. Display orders");
            Console.WriteLine("2. Add Order");
            Console.WriteLine("3. Edit Order");
            Console.WriteLine("4. Remove Order");
            Console.WriteLine("5. Quit");

            int choice = UserPrompts.GetChoiceFromUser("\n\nEnter choice: ", 5);

            if (choice == 5)
                Environment.Exit(0);

            DateTime date = UserPrompts.GetDateFromUser("What is the order date?");

            WorkflowFactory workflowFactory = new WorkflowFactory();
            workflowFactory.ChooseNewWorkflow(choice).Execute(date);
        }
    }
}
