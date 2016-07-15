using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryData.Interfaces;
using FlooringMasteryModels;

namespace FlooringMasteryData.FakeRepos
{
    public class FakeTaxRepository : ITaxRepository
    {
        private static List<TaxRate> _taxRates;

        public FakeTaxRepository()
        {
            _taxRates = new List<TaxRate>();

            if (_taxRates.Count == 0)
            {
                _taxRates = new List<TaxRate>
                {
                    new TaxRate() {Percent = 6.00m, StateAbbreviation = "PA", StateName = "Pennsylvania"},
                    new TaxRate() {Percent = 8.75m, StateAbbreviation = "OH", StateName = "Ohio"},
                    new TaxRate() {Percent = 7.65m, StateAbbreviation = "NJ", StateName = "New Jersey"},
                };
            }
        }

        public List<TaxRate> GetAllTaxRates()
        {
            return _taxRates;
        }
    }
}
