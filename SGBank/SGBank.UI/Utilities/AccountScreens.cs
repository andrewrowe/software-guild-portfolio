using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.UI.Utilities
{
    public static class AccountScreens
    {
        public static void PrintAccountDetails(Account account)
        {
            Console.WriteLine("Account Information");
            Console.WriteLine("===============================");
            Console.WriteLine($"Account number: {account.AccountNumber}");
            Console.WriteLine($"Name: {account.FirstName} {account.LastName}");
            Console.WriteLine($"Account Balance: {account.Balance:c}");
        }

        public static void WorkflowErrorScreen(string message)
        {
            Console.Clear();
            Console.WriteLine("An error occurred.\n{0}", message);
            UserPrompts.PressKeyForContinue();
        }

        public static void DepositDetails(DepositReceipt receipt)
        {
            Console.Clear();
            Console.WriteLine("Deposited {0:c} to account {1}.", 
                receipt.DepositAmount, receipt.AccountNumber);
            Console.WriteLine("New balance is {0}", receipt.NewBalance);
            UserPrompts.PressKeyForContinue();
        }

        public static void WithdrawDetails(WithdrawReceipt receipt)
        {
            Console.Clear();
            Console.WriteLine("Withdrew {0:c} from account {1}.",
                receipt.WithdrawAmount, receipt.AccountNumber);
            Console.WriteLine("New balance is {0}", receipt.NewBalance);
            UserPrompts.PressKeyForContinue();
        }

        public static void TransferDetails(TransferReceipt receipt)
        {
            Console.Clear();
            Console.WriteLine("Transferred {0:c} from account {1} into account {2}.",
                receipt.AmountTransferred, receipt.AccountWithdrew, receipt.AccountDeposited);
            Console.WriteLine("New balance is {0}", receipt.NewBalance);
            UserPrompts.PressKeyForContinue();
        }
    }
}
