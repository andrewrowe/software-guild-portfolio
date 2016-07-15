using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryData.RealRepos;
using FlooringMasteryModels;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void CanLoadOrdersSuccessfully()
        {
            var repo = new OrderRepository();
            var orders = repo.GetAllOrders(DateTime.Parse("5/15/16"));

            Assert.AreEqual(3, orders.Count);
        }

        [Test]
        public void CanLoadOrderSuccessfully()
        {
            var repo = new OrderRepository();
            var order = repo.LoadOrder(DateTime.Parse("5/15/16"), 1);

            Assert.AreEqual("Jack", order.FirstName);
        }

        [Test]
        public void CanDeleteOrderSuccessfully()
        {
            var repo = new OrderRepository();
            Order order = new Order()
            {
                Area = 700m,
                FirstName = "Andrew",
                LastName = "Rowe",
                OrderDate = DateTime.Parse("5/15/16"),
                OrderID = 2,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 3325m,
                TotalMaterialCost = 3605m,
                TotalTax = 46777.5m,
                TotalCost = 53707.5m
            };
            repo.DeleteOrder(order);
            var result = repo.LoadOrder(DateTime.Parse("5/15/16"), 2);

            Assert.AreEqual(OrderStatus.Deleted, result.Status);
        }

        [Test]
        public void CanSaveOrderSuccessfully()
        {
            var repo = new OrderRepository();
            Order order = new Order()
            {
                Area = 700m,
                FirstName = "Josh",
                LastName = "Bosh",
                OrderDate = DateTime.Parse("5/15/16"),
                OrderID = 4,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 3325m,
                TotalMaterialCost = 3605m,
                TotalTax = 46777.5m,
                TotalCost = 53707.5m
            };
            repo.SaveOrder(order);
            var orders = repo.GetAllOrders(DateTime.Parse("5/15/16"));

            Assert.AreEqual(4, orders.Count);
        }

        [Test]
        public void CanRemoveOrderSuccessfully()
        {
            var repo = new OrderRepository();
            Order order = new Order()
            {
                Area = 700m,
                FirstName = "Josh",
                LastName = "Bosh",
                OrderDate = DateTime.Parse("5/15/16"),
                OrderID = 4,
                ProductType = "Wood",
                State = "PA",
                Status = OrderStatus.Active,
                TotalLaborCost = 3325m,
                TotalMaterialCost = 3605m,
                TotalTax = 46777.5m,
                TotalCost = 53707.5m
            };
            repo.RemoveOrder(order);
            var orders = repo.GetAllOrders(DateTime.Parse("5/15/16"));

            Assert.AreEqual(3, orders.Count);
        }
    }
}
