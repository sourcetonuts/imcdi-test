using System.Diagnostics;
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

        public decimal GetTaxRate(string zipcode)
        {
            return _calculator.GetTaxRate(zipcode);
        }
    }
}
