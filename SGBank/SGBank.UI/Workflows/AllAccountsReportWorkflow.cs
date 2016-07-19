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
    public class AllAccountsReportWorkflow : IReportManagement
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager manager = new AccountManager();
            IOrderedEnumerable<Account> accounts = manager.GetAllAccountInfo();

            foreach (var account in accounts)
            {
                AccountScreens.PrintAccountDetails(account);
                Console.WriteLine();
            }

            UserPrompts.PressKeyForContinue();
        }
    }
}
