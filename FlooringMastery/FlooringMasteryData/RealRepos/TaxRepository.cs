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
    public class TaxRepository : ITaxRepository
    {
        private const string _filePath = @"DataFiles\TaxRates.txt";

        public List<TaxRate> GetAllTaxRates()
        {
            List<TaxRate> taxRates = new List<TaxRate>();

            var rows = File.ReadAllLines(_filePath);

            for (int i = 1; i < rows.Length; i++)
            {
                var columns = rows[i].Split(',');

                var taxRate = new TaxRate();
                taxRate.StateAbbreviation = columns[0];
                taxRate.StateName = columns[1];
                taxRate.Percent = decimal.Parse(columns[2]);

                taxRates.Add(taxRate);
            }

            return taxRates;
        }
    }
}
