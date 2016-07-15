using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryModels;

namespace FlooringMasteryData.RealRepos
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> GetAllOrders(DateTime date)
        {
            List<Order> orders = new List<Order>();

            string path = $"Orders_{date.ToString("MMddyyyy")}";

            if (File.Exists($"DataFiles\\{path}.txt"))
            {
                var rows = File.ReadAllLines($"DataFiles\\{path}.txt");

                for (int i = 1; i < rows.Length; i++)
                {
                    var columns = rows[i].Split(',');

                    var order = new Order();
                    order.FirstName = columns[0];
                    order.LastName = columns[1];
                    order.OrderDate = DateTime.Parse(columns[2]);
                    order.OrderID = int.Parse(columns[3]);
                    order.Area = decimal.Parse(columns[4]);
                    order.ProductType = columns[5];
                    order.Status = (OrderStatus) Enum.Parse(typeof(OrderStatus), columns[6]);
                    order.State = columns[7];
                    order.TotalLaborCost = decimal.Parse(columns[8]);
                    order.TotalMaterialCost = decimal.Parse(columns[9]);
                    order.TotalTax = decimal.Parse(columns[10]);
                    order.TotalCost = decimal.Parse(columns[11]);

                    orders.Add(order);
                }
            }

            return orders;
        }

        public Order LoadOrder(DateTime orderDate, int orderID)
        {
            List<Order> orders = GetAllOrders(orderDate);
            return orders.First(o => o.OrderID == orderID);
        }

        public void RemoveOrder(Order order)
        {
            List<Order> orders = GetAllOrders(order.OrderDate);
            var removeOrder = orders.First(o => o.OrderID == order.OrderID);
            orders.Remove(removeOrder);
            OverwriteFile(orders, order.OrderDate);
        }

        public void DeleteOrder(Order order)
        {
            List<Order> orders = GetAllOrders(order.OrderDate);
            var deleteOrder = orders.First(o => o.OrderID == order.OrderID);
            deleteOrder.Status = OrderStatus.Deleted;
            CheckFile(orders, order);
        }

        public void SaveOrder(Order order)
        {
            List<Order> orders = GetAllOrders(order.OrderDate);
            orders.Add(order);
            OverwriteFile(orders, order.OrderDate);
        }

        public int CreateOrderID(DateTime date)
        {
            List<Order> orders = GetAllOrders(date);

            if (orders.Count == 0)
                return 1;

            return orders.Max(o => o.OrderID + 1);
        }

        public void CheckFile(List<Order> orders, Order order)
        {
            bool active = false;
            foreach (var o in orders)
            {
                if (o.Status == OrderStatus.Active)
                {
                    active = true;
                    break;
                }
            }

            if (active)
                OverwriteFile(orders, order.OrderDate);
            else
                DeleteFile(order.OrderDate);
        }

        public void OverwriteFile(List<Order> orders, DateTime date)
        {
            string path = $"Orders_{date.ToString("MMddyyyy")}";

            if (File.Exists($"DataFiles\\{path}.txt"))
                File.Delete($"DataFiles\\{path}.txt");
            else
                CreateFile(date, path);

            using (var writer = File.CreateText($"DataFiles\\{path}.txt"))
            {
                writer.WriteLine(
                    "FirstName,LastName,OrderDate,OrderID,Area,ProductType,Status," +
                    "State,LaborCost,MaterialCost,Tax,TotalCost");

                foreach (var order in orders)
                {
                    writer.WriteLine($"{order.FirstName},{order.LastName},{order.OrderDate},{order.OrderID},{order.Area}," +
                                     $"{order.ProductType},{order.Status},{order.State},{order.TotalLaborCost}," +
                                     $"{order.TotalMaterialCost},{order.TotalTax},{order.TotalCost}");
                }
            }
        }

        public static void CreateFile(DateTime date, string path)
        {
            File.Create($"{path}.txt");
        }

        public static void DeleteFile(DateTime date)
        {
            string path = $"Orders_{date.ToString("MMddyyyy")}";

            if (File.Exists($"DataFiles\\{path}.txt"))
                File.Delete($"DataFiles\\{path}.txt");
        }
    }
}
