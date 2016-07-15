using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMasteryModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLaborCost { get; set; }
        public decimal TotalCost { get; set; }
        public OrderStatus Status { get; set; }
    }
}
