using System.Diagnostics;
using System.Threading.Tasks;
using TaxService.Interfaces;

namespace TaxService.Services
{
    public class TaxService
        : ITaxService
    {
        private readonly ITaxService _calculator;

        public TaxService( ITaxService calculator )
        {
            Debug.Assert( calculator != null );
            _calculator = calculator;
        }

        public async Task<decimal> GetTaxRate(string zipcode)
        {
            return await _calculator.GetTaxRate(zipcode);
        }
    }
}
