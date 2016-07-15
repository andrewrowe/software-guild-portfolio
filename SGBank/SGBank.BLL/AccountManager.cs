using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Data;
using SGBank.Models;

namespace SGBank.BLL
{
    public class AccountManager
    {
        private IAccountRepository _repo;

        public AccountManager()
        {
            _repo = AccountRepositoryFactory.GetAccountRepository();
        }

        public Response<Account> GetAccount(int accountNumber)
        {
            var result = new Response<Account>();

            try
            {
                var account = _repo.LoadAccount(accountNumber);

                if (account == null)
                {
                    result.Success = false;
                    result.Message = "Account was not found.";
                }
                else
                {
                    result.Success = true;
                    result.Data = account;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "There was an error. Please try again later.";
                //log.logError(ex.Message);
            }

            return result;
        }

        public IOrderedEnumerable<Account> GetAllAccountInfo()
        {
            var repo = new AccountRepository();
            List<Account> accounts = repo.GetAllAccounts();
            var alphabetical = accounts.OrderBy(a => a.LastName).ThenBy(a => a.FirstName);

            return alphabetical;
        }

        public Response<DepositReceipt> Deposit(decimal amount, Account account)
        {
            var response = new Response<DepositReceipt>();

            try
            {
                if (amount <= 0)
                {
                    response.Success = false;
                    response.Message = "Must provide a positive value.";
                }
                else
                {
                    account.Balance += amount;
                    var repo = new AccountRepository();
                    repo.UpdateAccount(account);

                    response.Success = true;
                    response.Data = new DepositReceipt();
                    response.Data.AccountNumber = account.AccountNumber;
                    response.Data.DepositAmount = amount;
                    response.Data.NewBalance = account.Balance;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Account is no longer valid.";
                //log.logError(ex.Message);
            }

            return response;
        }

        public Response<WithdrawReceipt> Withdraw(decimal amount, Account account)
        {
            var response = new Response<WithdrawReceipt>();

            try
            {
                if (amount <= 0 || amount > account.Balance)
                {
                    response.Success = false;
                    response.Message = "Must provide a positive value that is also less than your current balance.";
                }
                else
                {
                    account.Balance -= amount;
                    var repo = new AccountRepository();
                    repo.UpdateAccount(account);

                    response.Success = true;
                    response.Data = new WithdrawReceipt();
                    response.Data.AccountNumber = account.AccountNumber;
                    response.Data.WithdrawAmount = amount;
                    response.Data.NewBalance = account.Balance;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Account is no longer valid.";
                //log.logError(ex.Message);
            }

            return response;
        }

        public Response<TransferReceipt> Transfer(decimal amount, Account account1, Account account2)
        {
            var response = new Response<TransferReceipt>();
            var withdrawResponse = new Response<WithdrawReceipt>();
            var depositResponse = new Response<DepositReceipt>();

            try
            {
                withdrawResponse.Success = Withdraw(amount, account1).Success;

                if (withdrawResponse.Success)
                {
                    depositResponse.Success = Deposit(amount, account2).Success;

                    if (depositResponse.Success)
                    {
                        response.Success = true;
                        response.Data = new TransferReceipt();

                        response.Data.AmountTransferred = amount;
                        response.Data.AccountDeposited = account2.AccountNumber;
                        response.Data.AccountWithdrew = account1.AccountNumber;
                        response.Data.NewBalance = account1.Balance;
                    }

                    else
                    {
                        Deposit(amount, account1);
                        Console.WriteLine($"Deposit to Account {account2} failed.\n" +
                                          $"Money was put back into Account {account1}.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<Account> CreateAccount(decimal amount, string firstName, string lastName)
        {
            var response = new Response<Account>();
            AccountRepository repo = new AccountRepository();

            try
            {
                response.Data = new Account();

                response.Data.FirstName = firstName;
                response.Data.LastName = lastName;
                response.Data.Balance = amount;

                repo.SaveAccount(response.Data);
                response.Success = true;
            }
            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public void DeleteAccount(int accountNumber)
        {
            var response = GetAccount(accountNumber);

            if (response.Success && response.Data.Balance == 0)
            {
                _repo.DeleteAccount(response.Data);
            }
        }

        public decimal FindAverageBalance()
        {
            AccountRepository repo = new AccountRepository();
            List<Account> accounts = repo.GetAllAccounts();

            return accounts.Average(a => a.Balance);
        }

        public Account FindMaxBalance()
        {
            AccountRepository repo = new AccountRepository();
            List<Account> accounts = repo.GetAllAccounts();

            var maxVal = accounts.Max(a => a.Balance);
            return accounts.First(a => a.Balance == maxVal);
        }

        public Account FindMinBalance()
        {
            AccountRepository repo = new AccountRepository();
            List<Account> accounts = repo.GetAllAccounts();

            var minVal = accounts.Min(a => a.Balance);
            return accounts.First(a => a.Balance == minVal);
        }
    }
}
