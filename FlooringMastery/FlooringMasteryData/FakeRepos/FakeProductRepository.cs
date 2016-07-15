using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryModels;

namespace FlooringMasteryData.FakeRepos
{
    public class FakeProductRepository : IProductRepository
    {
        private static List<Product> _products;

        public FakeProductRepository()
        {
            _products = new List<Product>();

            if (_products.Count == 0)
            {
                _products = new List<Product>
                {
                    new Product() {CostPerSquareFoot = 2.15m, LaborPerSquareFoot = 1.50m, ProductType = "Lime tile"},
                    new Product() {CostPerSquareFoot = 3.00m, LaborPerSquareFoot = 2.00m, ProductType = "Maple Wood"},
                    new Product() {CostPerSquareFoot = 1.15m, LaborPerSquareFoot = 0.50m, ProductType = "Linoleum"},
                    new Product() {CostPerSquareFoot = 5.45m, LaborPerSquareFoot = 2.20m, ProductType = "Metal"}
                };
            }
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }
    }
}
