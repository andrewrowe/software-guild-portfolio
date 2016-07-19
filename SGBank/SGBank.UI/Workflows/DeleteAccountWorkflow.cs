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
    public class DeleteAccountWorkflow : IWorkflow
    {
        public void Execute()
        {
            AccountManager manager = new AccountManager();
            MainMenu mainMenu = new MainMenu();

            Console.Clear();
            int accountNumber = UserPrompts.GetIntFromUser("Please enter the account to delete: ");
            Console.Clear();

            var response = manager.GetAccount(accountNumber);

            if (response.Success)
            {
                if (response.Data.Balance != 0)
                {
                    AccountScreens.WorkflowErrorScreen("Please withdraw funds before proceeding." +
                                                       "\nCannot delete an account with available funds.");
                    mainMenu.Execute();
                }

                AccountScreens.PrintAccountDetails(response.Data);
                bool confirm = UserPrompts.AskForConfirmation("\nAre you sure you want to delete this account?");

                if (confirm && response.Data.Balance == 0)
                {
                    manager.DeleteAccount(accountNumber);
                    Console.WriteLine("Account {0} deleted successfully.", accountNumber);
                    UserPrompts.PressKeyForContinue();
                }
                else
                {
                    mainMenu = new MainMenu();
                    mainMenu.Execute();
                }
            }
            else
            {
                AccountScreens.WorkflowErrorScreen(response.Message);
            }
        }
    }
}
