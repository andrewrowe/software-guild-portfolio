using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models
{
    public class TransferReceipt
    {
        public int AccountWithdrew { get; set; }
        public int AccountDeposited { get; set; }
        public decimal AmountTransferred { get; set; }
        public decimal NewBalance { get; set; }
    }
}
