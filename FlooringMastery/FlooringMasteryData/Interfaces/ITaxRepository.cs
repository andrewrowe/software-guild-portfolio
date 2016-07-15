using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMasteryModels;

namespace FlooringMasteryData.Interfaces
{
    public interface ITaxRepository
    {
        List<TaxRate> GetAllTaxRates();
    }
}
