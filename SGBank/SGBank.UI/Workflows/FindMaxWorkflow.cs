using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.BLL;
using SGBank.Models;
using SGBank.UI.Interfaces;
using SGBank.UI.Utilities;

namespace SGBank.UI.Workflows
{
    public class FindMaxWorkflow : IReportManagement
    {
        public void Execute()
        {
            var manager = new AccountManager();
            Account maxAccount = manager.FindMaxBalance();

            Console.Clear();
            AccountScreens.PrintAccountDetails(maxAccount);
            UserPrompts.PressKeyForContinue();
        }
    }
}
