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
    public class FindMinWorkflow : IReportManagement
    {
        public void Execute()
        {
            var manager = new AccountManager();
            Account minAccount = manager.FindMinBalance();

            Console.Clear();
            AccountScreens.PrintAccountDetails(minAccount);
            UserPrompts.PressKeyForContinue();
        }
    }
}
