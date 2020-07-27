using System;
using System.Collections.Generic;
using TaxService.Interfaces;

namespace TaxService.Services
{
    public class Order
        : IOrder
    {
        public Order()
        {
            OrderId = Guid.NewGuid();
            TotalPreTax = 0m;
            Tax = 0m;
            Total = 0m;
            LineItems = new List<ILineItem>();
        }

        public Guid OrderId { get; set; }

        public IList<ILineItem> LineItems { get; }

        public decimal TotalPreTax { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public void AddLineItem(IOrderItem item, decimal quantity = 1)
        {
            var lineitem = new LineItem(item, quantity);
            LineItems.Add(lineitem);
        }

        public void UpdateTotals(decimal subtotal, decimal tax)
        {
            TotalPreTax = subtotal;
            Tax = tax;
            Total = TotalPreTax + Tax;
        }
    }
}
