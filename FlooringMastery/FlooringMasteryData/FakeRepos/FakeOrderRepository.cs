using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryModels;

namespace FlooringMasteryData
{
    public class FakeOrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order>()
                {
                    new Order() {FirstName = "Joe", LastName = "Smith", Area = 550, OrderDate = DateTime.Parse("5/12/2016"), OrderID = 1,
                    ProductType = "Lime tile", Status = OrderStatus.Active, State = "PA",
                    TotalLaborCost = 5, TotalMaterialCost = 10, TotalTax = 5, TotalCost = 20 },
                    new Order() {FirstName = "Jack", LastName = "Reacher", Area = 200, OrderDate = DateTime.Parse("5/12/2016"), OrderID = 2,
                    ProductType = "Maple Wood", Status = OrderStatus.Active, State = "OH",
                    TotalLaborCost = 500, TotalMaterialCost = 10000, TotalTax = 500, TotalCost = 20000 },
                    new Order() {FirstName = "Jill", LastName = "Doe", Area = 340, OrderDate = DateTime.Parse("5/12/2016"), OrderID = 3,
                    ProductType = "Lime tile", Status = OrderStatus.Active, State = "PA",
                    TotalLaborCost = 300, TotalMaterialCost = 175, TotalTax = 150, TotalCost = 625 },
                    new Order() {FirstName = "Captain", LastName = "America", Area = 700, OrderDate = DateTime.Parse("5/14/2016"), OrderID = 1,
                    ProductType = "Linoleum", Status = OrderStatus.Active, State = "NJ",
                    TotalLaborCost = 150, TotalMaterialCost = 100, TotalTax = 100, TotalCost = 350 },
                    new Order() {FirstName = "Harris", LastName = "Wittels", Area = 234, OrderDate = DateTime.Parse("5/15/2016"), OrderID = 2,
                    ProductType = "Maple Wood", Status = OrderStatus.Active, State = "PA",
                    TotalLaborCost = 564, TotalMaterialCost = 450, TotalTax = 123, TotalCost = 1500 },
                    new Order() {FirstName = "Tony", LastName = "Stark", Area = 650, OrderDate = DateTime.Parse("5/16/2016"), OrderID = 1,
                    ProductType = "Metal", Status = OrderStatus.Active, State = "NJ",
                    TotalLaborCost = 1000, TotalMaterialCost = 300, TotalTax = 200, TotalCost = 1500 },
                    new Order() {FirstName = "Elijah", LastName = "Wood", Area = 750, OrderDate = DateTime.Parse("5/16/2016"), OrderID = 2,
                    ProductType = "Lime tile", Status = OrderStatus.Active, State = "OH",
                    TotalLaborCost = 550, TotalMaterialCost = 200, TotalTax = 100, TotalCost = 850 },
                    new Order() {FirstName = "Andy", LastName = "Samberg", Area = 750, OrderDate = DateTime.Parse("5/16/2016"), OrderID = 3,
                    ProductType = "Lime tile", Status = OrderStatus.Active, State = "OH",
                    TotalLaborCost = 550, TotalMaterialCost = 200, TotalTax = 100, TotalCost = 850 },
                };

        public List<Order> GetAllOrders(DateTime date)
        {
            return _orders.Where(o => o.OrderDate == date).OrderBy(o => o.OrderID).ToList();
        }

        public Order LoadOrder(DateTime date, int orderID)
        {
            return _orders.First(o => o.OrderDate == date && o.OrderID == orderID);
        }

        public void RemoveOrder(Order order)
        {
            _orders.Remove(order);
        }

        public void DeleteOrder(Order order)
        {
            order.Status = OrderStatus.Deleted;
        }

        public void SaveOrder(Order order)
        {
            _orders.Add(order);
        }

        public int CreateOrderID(DateTime date)
        {
            List<Order> orders = GetAllOrders(date);

            if (orders.Count == 0)
                return 1;

            return orders.Max(o => o.OrderID + 1);
        }
    }
}
