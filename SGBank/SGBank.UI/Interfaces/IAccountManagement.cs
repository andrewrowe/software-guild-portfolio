using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.UI.Interfaces
{
    public interface IAccountManagement
    {
        void Execute(Account account);
    }
}
