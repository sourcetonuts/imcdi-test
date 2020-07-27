using System.Linq;
using System.Threading.Tasks;

namespace TaxService.Interfaces
{
    public interface ITaxService
    {
        Task<decimal> GetTaxRate(string zipcode);

        void CalculateFromOrder(IOrder order, decimal taxrate)
        {
            var subtotal = order.LineItems.Sum(li => li.Price);
            var tax = 0m;
            if (taxrate > 0m)
            {
                tax = order.LineItems
                    .Where(li => li.IsTaxible)
                    .Sum(li => li.Price * taxrate);
            }

            order.UpdateTotals(subtotal, tax);
        }
    }
}
