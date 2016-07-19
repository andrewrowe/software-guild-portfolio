using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.UI.Workflows;

namespace SGBank.UI.Interfaces
{
    public class WorkflowFactory
    {
        public IWorkflow CreateNewWorkflow(string input)
        {
            switch (input)
            {
                case "1":
                    return new CreateAccountWorkflow();
                case "2":
                    return new DeleteAccountWorkflow();
                case "3":
                    return new LookupWorkflow();
                default:
                    return new ReportsLookupWorkflow();
            }
        }
    }
}
