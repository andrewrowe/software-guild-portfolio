using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.UI.Interfaces;

namespace SGBank.UI.Workflows
{
    public class MainMenu
    {
        public void Execute()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to SG Corp Bank");
                Console.WriteLine("=================================");
                Console.WriteLine("\n1. Create Account");
                Console.WriteLine("2. Delete Account");
                Console.WriteLine("3. Look up Account");
                Console.WriteLine("4. Bank Reports");
                Console.WriteLine("\n(Q) to Quit");

                Console.WriteLine("\n\nEnter Choice: ");
                string input = Console.ReadLine();

                if (input.Length == 0 || input.Substring(0, 1).ToUpper() == "Q")
                    break;

                WorkflowFactory workflowFactory = new WorkflowFactory();
                workflowFactory.CreateNewWorkflow(input).Execute();
            } while (true);
        }
    }
}
