using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Data;
using SGBank.Models;

namespace SGBank.Tests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        [Test]
        public void CanLoadAllAccounts()
        {
            var repo = new AccountRepository();
            var accounts = repo.GetAllAccounts();

            Assert.AreEqual(4, accounts.Count);
        }

        [TestCase(1, "Joe")]
        [TestCase(2, "Jane")]
        public void CanLoadSpecificAccount(int accountNumber, string expected)
        {
            var repo = new AccountRepository();
            var account = repo.LoadAccount(accountNumber);

            Assert.AreEqual(expected, account.FirstName);
        }

        [Test]
        public void UpdateAccountSucceeds()
        {
            var repo = new AccountRepository();
            var accountToUpdate = repo.LoadAccount(1);
            accountToUpdate.Balance = 500.00m;
            repo.UpdateAccount(accountToUpdate);

            var result = repo.LoadAccount(1);
            Assert.AreEqual(500.00m, result.Balance);
        }

        [TestCase("Joe", "Schmo", 2000)]
        public void CreateAccountSucceeds(string firstName, string lastName, decimal amount)
        {
            var repo = new AccountRepository();
            repo.SaveAccount(new Account {FirstName = firstName, LastName = lastName, Balance = amount});

            var result = repo.LoadAccount(5);

            Assert.AreEqual("Joe", result.FirstName);
            Assert.AreEqual("Schmo", result.LastName);
            Assert.AreEqual(2000m, result.Balance);
        }

        [TestCase("Joe", "Schmo", 2000, 5)]
        public void DeleteAccountSucceeds(string firstName, string lastName, decimal amount, int accountNumber)
        {
            var repo = new AccountRepository();
            repo.DeleteAccount(new Account {FirstName = firstName, LastName = lastName, AccountNumber = accountNumber, Balance = amount});

            Assert.IsTrue(repo.GetAllAccounts().Count == 4);
        }
    }
}
