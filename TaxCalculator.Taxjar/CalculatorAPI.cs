using System.Threading.Tasks;
using Taxjar;
using TaxService.Interfaces;

namespace TaxCalculator.Taxjar
{
    public class CalculatorAPI
        : CalculatorBaseTaxjar
        , ITaxService
    {
        protected readonly TaxjarApi _client = new TaxjarApi( API_KEY );

        public async Task<decimal> GetTaxRate(string zipcode)
        {
            await Task.Delay(0);
            var rates = _client.RatesForLocation( zipcode );
            return rates.CombinedRate;
        }
    }
}
