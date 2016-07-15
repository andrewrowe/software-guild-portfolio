using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryBLL;
using FlooringMasteryModels;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class OrderManagerTests
    {
        [Test]
        public void GetAllOrdersReturnsSuccess()
        {
            var manager = new OrderManager();
            var orders = manager.GetAllOrders(DateTime.Parse("5/15/16"));

            Assert.AreEqual(3, orders.Count);
        }

        [Test]
        public void CreateOrderReturnsSuccess()
        {
            var manager = new OrderManager();
            var response = manager.CreateOrder("Josh", "Bosh", "Wood", 500m, DateTime.Parse("5/11/16"), "PA");

            Assert.IsTrue(response.Success);
            Assert.AreEqual("PA", response.Data.State);
            Assert.AreEqual(500, response.Data.Area);
            Assert.AreEqual(DateTime.Parse("5/11/16"), response.Data.OrderDate);
            Assert.AreEqual(1, response.Data.OrderID);
        }

        [Test]
        public void UpdateOrderIDReturnsSuccess()
        {
            var manager = new OrderManager();
            Order order = new Order()
            {
                Area = 500m,
                FirstName = "Judd",
                LastName = "Apatow",
                OrderDate = DateTime.Parse("5/19/16"),
                OrderID = 0,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 800m,
                TotalMaterialCost = 1000m,
                TotalTax = 200m,
                TotalCost = 2000m
            };
            var response = manager.UpdateOrderID(order);

            Assert.AreEqual(1, response.Data.OrderID);
        }

        [Test]
        public void LoadOrderReturnsSuccess()
        {
            var manager = new OrderManager();
            var response = manager.LoadOrder(DateTime.Parse("5/11/16"), 1);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public void NoOrderForLoadOrderReturnsFail()
        {
            var manager = new OrderManager();
            var response = manager.LoadOrder(DateTime.Parse("5/11/16"), 9999);

            Assert.IsTrue(!response.Success);
        }

        [Test]
        public void SaveOrderReturnsSuccess()
        {
            var manager = new OrderManager();
            Order order = new Order()
            {
                Area = 500m,
                FirstName = "Josh",
                LastName = "Bosh",
                OrderDate = DateTime.Parse("5/11/16"),
                OrderID = 1,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 800m,
                TotalMaterialCost = 1000m,
                TotalTax = 200m,
                TotalCost = 2000m
            };
            var response = manager.SaveOrder(order);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public void DeleteOrderReturnsSuccess()
        {
            var manager = new OrderManager();
            Order order = new Order()
            {
                Area = 500m,
                FirstName = "Josh",
                LastName = "Bosh",
                OrderDate = DateTime.Parse("5/11/16"),
                OrderID = 1,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 800m,
                TotalMaterialCost = 1000m,
                TotalTax = 200m,
                TotalCost = 2000m
            };
            var response = manager.DeleteOrder(order);

            Assert.IsTrue(response.Success);
        }

        [Test]
        public void RemoveOrderReturnsSuccess()
        {
            var manager = new OrderManager();
            Order order = new Order()
            {
                Area = 500m,
                FirstName = "Josh",
                LastName = "Bosh",
                OrderDate = DateTime.Parse("5/11/16"),
                OrderID = 1,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 800m,
                TotalMaterialCost = 1000m,
                TotalTax = 200m,
                TotalCost = 2000m
            };
            var response = manager.RemoveOrder(order);

            Assert.IsTrue(response.Success);
        }
    }
}
