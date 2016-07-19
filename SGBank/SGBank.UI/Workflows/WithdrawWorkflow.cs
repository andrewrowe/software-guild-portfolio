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
    public class WithdrawWorkflow : IAccountManagement
    {
        public void Execute(Account account)
        {
            decimal amount = UserPrompts.GetDecimalFromUser("Please provide a withdrawl amount: ");

            var manager = new AccountManager();
            var response = manager.Withdraw(amount, account);

            if (response.Success)
            {
                AccountScreens.WithdrawDetails(response.Data);
            }
            else
            {
                AccountScreens.WorkflowErrorScreen(response.Message);
            }
        }
    }
}
