using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.UI.Workflows;

namespace SGBank.UI.Interfaces
{
    public class ReportManagementFactory
    {
        public IReportManagement CreateNewReport(string input)
        {
            switch (input)
            {
                case "1":
                    return new AllAccountsReportWorkflow();
                case "2":
                    return new FindAverageWorkflow();
                case "3":
                    return new FindMaxWorkflow();
                default:
                    return new FindMinWorkflow();
            }
        }
    }
}
