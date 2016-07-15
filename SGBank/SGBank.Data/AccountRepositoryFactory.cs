using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public static class AccountRepositoryFactory
    {
        public static IAccountRepository GetAccountRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Test":
                    return new FakeAccountRepository();
                default:
                    return new AccountRepository();
            }
        }
    }
}
