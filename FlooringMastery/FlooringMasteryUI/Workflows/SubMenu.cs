using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryUI.Interfaces;
using FlooringMasteryUI.Utilities;

namespace FlooringMasteryUI.Workflows
{
    public class SubMenu
    {
        public static void Execute(DateTime date)
        {
            Console.WriteLine("\n1. Add Order");
            Console.WriteLine("2. Edit Order");
            Console.WriteLine("3. Remove Order");
            Console.WriteLine("4. Main Menu");

            int choice = UserPrompts.GetChoiceFromUser("\nEnter choice: ", 4);

            if (choice == 4)
                MainMenu.Execute();

            WorkflowFactory workflowFactory = new WorkflowFactory();
            workflowFactory.ChooseNewWorkflow(choice + 1).Execute(date);
        }
    }
}
