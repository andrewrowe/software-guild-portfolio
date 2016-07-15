using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.UI.Interfaces;
using SGBank.UI.Utilities;

namespace SGBank.UI.Workflows
{
    public class ReportsLookupWorkflow : IWorkflow, IReportManagement
    {
        public void Execute()
        {

            do
            {
                Console.Clear();

                Console.WriteLine("1. Get all accounts");
                Console.WriteLine("2. Average account balance");
                Console.WriteLine("3. Highest account balance");
                Console.WriteLine("4. Lowest account balance");
                Console.WriteLine("\n(Q) to return to main menu");

                string input = UserPrompts.GetStringFromUser("\nEnter Choice: ");

                if (input.Length == 0 || input.Substring(0, 1).ToUpper() == "Q")
                    break;

                ReportManagementFactory reportManagementFactory = new ReportManagementFactory();
                reportManagementFactory.CreateNewReport(input).Execute();

            } while (true);
        }
    }
}
