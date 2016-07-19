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
    public class TransferWorkflow : IAccountManagement
    {
        public void Execute(Account account1)
        {
            decimal amount = UserPrompts.GetDecimalFromUser("Please provide a transfer amount: ");
            int accountNumber = UserPrompts.GetIntFromUser("What account would you like to transfer to: ");

            var manager = new AccountManager();
            Account account2 = manager.GetAccount(accountNumber).Data;
            var response = manager.Transfer(amount, account1, account2);

            if (response.Success)
            {
                AccountScreens.TransferDetails(response.Data);
            }
            else
            {
                AccountScreens.WorkflowErrorScreen(response.Message);
            }
        }
    }
}
