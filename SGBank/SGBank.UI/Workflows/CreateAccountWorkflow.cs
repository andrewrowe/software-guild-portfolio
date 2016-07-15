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
    public class CreateAccountWorkflow : IWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            string firstName = UserPrompts.GetStringFromUser("Please enter first name: ");
            Console.Clear();
            string lastName = UserPrompts.GetStringFromUser("Please enter last name: ");

            decimal amount = UserPrompts.GetDecimalFromUser("Please enter starting balance: ");

            var manager = new AccountManager();
            var response = manager.CreateAccount(amount, firstName, lastName);

            AccountScreens.PrintAccountDetails(response.Data);
            UserPrompts.PressKeyForContinue();
        }
    }
}
