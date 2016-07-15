using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.Data
{
    public interface IAccountRepository
    {
        List<Account> GetAllAccounts();
        Account LoadAccount(int accountNumber);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);
        void SaveAccount(Account newAccount);
    }
}
