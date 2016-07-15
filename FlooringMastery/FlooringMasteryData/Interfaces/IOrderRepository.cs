using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryModels;

namespace FlooringMasteryData.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders(DateTime date);
        Order LoadOrder(DateTime orderDate, int orderID);
        void DeleteOrder(Order order);
        void SaveOrder(Order order);
        int CreateOrderID(DateTime date);
        void RemoveOrder(Order order);
    }
}
