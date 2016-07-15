using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData;
using FlooringMasteryData.Interfaces;
using FlooringMasteryData.RealRepos;
using FlooringMasteryModels;

namespace FlooringMasteryBLL
{
    public class OrderManager
    {
        private const string _errorLogFilePath = @"DataFiles\ErrorLog.txt";

        private IOrderRepository _orderRepo;
        public List<TaxRate> taxRates;
        public List<Product> products;

        public OrderManager()
        {
            _orderRepo = OrderRepositoryFactory.GetOrderRepository();
            taxRates = TaxRepositoryFactory.GetTaxRepository().GetAllTaxRates();
            products = ProductRepositoryFactory.GetProductRepository().GetAllProducts();
        }

        public List<Order> GetAllOrders(DateTime date)
        {
            return _orderRepo.GetAllOrders(date);
        }

        public Response<Order> CreateOrder(string firstName, string lastName, string productType, decimal area, DateTime orderDate, string state)
        {
            var response = new Response<Order>();

            try
            {
                response.Data = new Order();

                response.Data.FirstName = firstName;
                response.Data.LastName = lastName;
                response.Data.OrderDate = orderDate;
                response.Data.ProductType = productType;
                response.Data.State = state;
                response.Data.Area = area;
                response.Data.TotalLaborCost = CalculateLaborCost(area, productType);
                response.Data.TotalMaterialCost = CalculateMaterialCost(area, productType);
                response.Data.TotalTax = CalculateTax(response.Data.TotalLaborCost, response.Data.TotalMaterialCost, state);
                response.Data.TotalCost =
                    CalculateTotal(response.Data.TotalLaborCost, response.Data.TotalMaterialCost,
                        response.Data.TotalTax);
                response.Data.Status = OrderStatus.Active;
                response.Data.OrderID = _orderRepo.CreateOrderID(response.Data.OrderDate);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error.\nOrder was not created. Please try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public Response<Order> UpdateOrderID(Order order)
        {
            var response = new Response<Order>();

            try
            {
                response.Data = order;
                order.OrderID = _orderRepo.CreateOrderID(order.OrderDate);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error.\nOrder ID could not be updated. Please try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public Response<Order> RemoveOrder(Order order)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepo.RemoveOrder(order);
                response.Success = true;
                response.Message = "Order removed successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error.\nOrder was not removed. Please try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public Response<Order> LoadOrder(DateTime orderDate, int orderID)
        {
            var response = new Response<Order>();

            try
            {
                response.Data = _orderRepo.LoadOrder(orderDate, orderID);
                response.Success = true;
                if (response.Data.Status != OrderStatus.Active)
                {
                    response.Success = false;
                    response.Message =
                        "There was an error.\nThis order is no longer active. Please check order date and ID, and try again.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error.\nOrder could not be loaded. Please check order date and ID, and try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public Response<Order> SaveOrder(Order order)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepo.SaveOrder(order);
                response.Success = true;
                response.Message = "Order saved successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Order was not saved. Please try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public Response<Order> DeleteOrder(Order order)
        {
            var response = new Response<Order>();

            try
            {
                _orderRepo.DeleteOrder(order);
                response.Message = "Order deleted successfully!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "There was an error.\nOrder was not deleted. Please try again.";
                DateTime errorTime = DateTime.Now;
                WriteToErrorLog(errorTime, ex.Message);
            }

            return response;
        }

        public static void WriteToErrorLog(DateTime date, string errorMessage)
        {
            File.AppendAllText(_errorLogFilePath, $"{date}\t\t{errorMessage}{String.Format(Environment.NewLine)}");
        }

        public TaxRate GetTaxRate(string state)
        {
            return taxRates.First(t => t.StateAbbreviation == state);
        }

        public Product GetProduct(string productType)
        {
            return products.First(p => p.ProductType == productType);
        }


        //Calculations
        public decimal CalculateLaborCost(decimal area, string productType)
        {
            var product = products.First(p => p.ProductType == productType);

            return product.LaborPerSquareFoot * area;
        }

        public decimal CalculateMaterialCost(decimal area, string productType)
        {
            var product = products.First(p => p.ProductType == productType);

            return product.CostPerSquareFoot * area;
        }

        public decimal CalculateTax(decimal laborCost, decimal materialCost, string state)
        {
            var tax = taxRates.First(t => t.StateAbbreviation == state);

            return (tax.Percent / 100) * (laborCost + materialCost);
        }

        public decimal CalculateTotal(decimal laborCost, decimal materialCost, decimal tax)
        {
            return laborCost + materialCost + tax;
        }
    }
}
