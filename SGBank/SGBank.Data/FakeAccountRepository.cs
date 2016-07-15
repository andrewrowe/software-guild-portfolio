using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.Data
{
    class FakeAccountRepository : IAccountRepository
    {
        private static List<Account> accounts;

        public FakeAccountRepository()
        { 
            accounts = new List<Account>();

            if (accounts.Count == 0)
            {
                accounts = new List<Account>
                {
                    new Account() {AccountNumber = 1, FirstName = "Dave", LastName = "Balzer", Balance = 2000m},
                    new Account() {AccountNumber = 2, FirstName = "Joe", LastName = "Dirt", Balance = 35000m},
                    new Account() {AccountNumber = 3, FirstName = "Will", LastName = "Ferrell", Balance = 600m},
                    new Account() {AccountNumber = 4, FirstName = "David", LastName = "Blaine", Balance = 5000m},
                };
            }
        }
        public List<Account> GetAllAccounts()
        {
            return accounts;
        }

        public Account LoadAccount(int accountNumber)
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            var accountToUpdate = accounts.First(a => a.AccountNumber == account.AccountNumber);
            accountToUpdate.FirstName = account.FirstName;
            accountToUpdate.LastName = account.LastName;
            accountToUpdate.Balance = account.Balance;
        }

        public void DeleteAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public void SaveAccount(Account newAccount)
        {
            throw new NotImplementedException();
        }
    }
}
