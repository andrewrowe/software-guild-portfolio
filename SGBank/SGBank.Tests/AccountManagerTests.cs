using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.Data;
using SGBank.Models;

namespace SGBank.Tests
{
    [TestFixture]
    public class AccountManagerTests
    {
        [Test]
        public void FoundAccountReturnsSuccess()
        {
            var manager = new AccountManager();
            var response = manager.GetAccount(2);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(2, response.Data.AccountNumber);
            Assert.AreEqual("Bob", response.Data.FirstName);
        }

        [Test]
        public void NotFoundAccountReturnsFail()
        {
            var manager = new AccountManager();
            var response = manager.GetAccount(9999);

            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DepositReturnsCorrectBalance()
        {
            var manager = new AccountManager();
            Account testAccount = new Account();

            testAccount.AccountNumber = 1;
            testAccount.FirstName = "Joe";
            testAccount.LastName = "Schmo";
            testAccount.Balance = 200.00m;

            var response = manager.Deposit(10m, testAccount);
            Assert.AreEqual(210.00m, response.Data.NewBalance);
        }

        [Test]
        public void WithdrawReturnsCorrectBalance()
        {
            var manager = new AccountManager();
            Account testAccount = new Account();

            testAccount.AccountNumber = 1;
            testAccount.FirstName = "Joe";
            testAccount.LastName = "Schmo";
            testAccount.Balance = 200.00m;

            var response = manager.Withdraw(10m, testAccount);
            Assert.AreEqual(190.00m, response.Data.NewBalance);
        }

        [Test]
        public void TransferReturnsCorrectBalances()
        {
            var manager = new AccountManager();
            Account testAccount1 = new Account();
            Account testAccount2 = new Account();

            testAccount1.AccountNumber = 1;
            testAccount1.FirstName = "Joe";
            testAccount1.LastName = "Schmo";
            testAccount1.Balance = 500.00m;

            testAccount2.AccountNumber = 2;
            testAccount2.FirstName = "Jane";
            testAccount2.LastName = "Doe";
            testAccount2.Balance = 250.00m;

            var response = manager.Transfer(120, testAccount1, testAccount2);
            Assert.AreEqual(380.00m, response.Data.NewBalance);

            var checkAccount = manager.GetAccount(testAccount2.AccountNumber);
            Assert.AreEqual(370.00m, checkAccount.Data.Balance);
        }
    }
}
