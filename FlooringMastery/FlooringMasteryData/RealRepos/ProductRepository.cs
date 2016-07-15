using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryModels;

namespace FlooringMasteryData.RealRepos
{
    public class ProductRepository : IProductRepository
    {
        private const string _filePath = @"DataFiles\Products.txt";

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            var rows = File.ReadAllLines(_filePath);

            for (int i = 1; i < rows.Length; i++)
            {
                var columns = rows[i].Split(',');

                var product = new Product();
                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(product);
            }

            return products;
        }
    }
}
