using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.UI.Workflows;

namespace SGBank.UI.Interfaces
{
    public class AccountManagementFactory
    {
        public IAccountManagement CreateNewAccountWorkflow(string input)
        {
            switch (input)
            {
                case "1":
                    return new DepositWorkflow();
                case "2":
                    return new WithdrawWorkflow();
                default:
                    return new TransferWorkflow();
            }
        }
    }
}
