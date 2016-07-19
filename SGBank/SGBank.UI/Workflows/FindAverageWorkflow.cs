using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.UI.Interfaces;
using SGBank.UI.Utilities;

namespace SGBank.UI.Workflows
{
    public class FindAverageWorkflow : IReportManagement
    {
        public void Execute()
        {
            var manager = new AccountManager();
            decimal average = manager.FindAverageBalance();

            Console.Clear();
            Console.WriteLine($"The average account balance is {average:c}.");
            UserPrompts.PressKeyForContinue();
        }
    }
}
