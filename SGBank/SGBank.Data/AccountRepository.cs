using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.Data
{
    public class AccountRepository : IAccountRepository
    {
        private const string _filePath = @"DataFiles\Bank.txt";

        public List<Account> GetAllAccounts()
        {
            List<Account> results = new List<Account>();

            var rows = File.ReadAllLines(_filePath);

            for (int i = 1; i < rows.Length; i++)
            {
                var columns = rows[i].Split(',');

                var account = new Account();
                account.AccountNumber = int.Parse(columns[0]);
                account.FirstName = columns[1];
                account.LastName = columns[2];
                account.Balance = decimal.Parse(columns[3]);

                results.Add(account);
            }

            return results;
        }

        private int GetNewAccountNumber()
        {
            List<Account> accounts = GetAllAccounts();

            return accounts.Max(m => m.AccountNumber) + 1;
        }

        public Account LoadAccount(int accountNumber)
        {
            List<Account> accounts = GetAllAccounts();
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            var accounts = GetAllAccounts();

            var accountToUpdate = accounts.First(a => a.AccountNumber == account.AccountNumber);

            accountToUpdate.FirstName = account.FirstName;
            accountToUpdate.LastName = account.LastName;
            accountToUpdate.Balance = account.Balance;

            OverwriteFile(accounts);
        }

        public void SaveAccount(Account newAccount)
        {
            List<Account> accounts = GetAllAccounts();
            newAccount.AccountNumber = GetNewAccountNumber();
            accounts.Add(newAccount);
            OverwriteFile(accounts);
        }

        public void DeleteAccount(Account account)
        {
            List<Account> accounts = GetAllAccounts();
            var accountToDelete = accounts.First(a => a.AccountNumber == account.AccountNumber);
            accounts.Remove(accountToDelete);
            OverwriteFile(accounts);
        }

        private void OverwriteFile(List<Account> accounts)
        {
            File.Delete(_filePath);

            using (var writer = File.CreateText(_filePath))
            {
                writer.WriteLine("AccountNumber,FirstName,LastName,Balance");
                foreach (var account in accounts)
                {
                    writer.WriteLine("{0},{1},{2},{3}",
                        account.AccountNumber,
                        account.FirstName,
                        account.LastName,
                        account.Balance);
                }
            }
        }
    }
}
