using System.Linq;
using Taxjar;
using TaxService.Interfaces;

namespace TaxCalculator.Taxjar
{
    public class Calculator
        : ITaxService
    {
        private readonly TaxjarApi _client = new TaxjarApi("5da2f821eee4035db4771edab942a4cc");

        public decimal GetTaxRate(string zipcode)
        {
            var rates = _client.RatesForLocation( zipcode );
            return rates.CombinedRate;
        }

    }
}
