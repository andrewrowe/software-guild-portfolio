using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryUI.Workflows;

namespace FlooringMasteryUI.Interfaces
{
    public class WorkflowFactory
    {
        public IWorkflow ChooseNewWorkflow(int choice)
        {
            switch (choice)
            {
                case 1:
                    return new DisplayOrdersWorkflow();
                case 2:
                    return new AddOrderWorkflow();
                case 3:
                    return new EditOrderWorkflow();
                default:
                    return new RemoveOrderWorkflow();
            }
        }
    }
}
