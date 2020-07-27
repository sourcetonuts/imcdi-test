using System;
using System.Collections.Generic;

namespace TaxService.Interfaces
{
    public interface IOrder
    {
        Guid OrderId { get; }

        IList<ILineItem> LineItems { get; }

        decimal TotalPreTax { get; }

        decimal Tax { get; }

        decimal Total { get; }

        void AddLineItem(IOrderItem item, decimal quantity = 1);

        void UpdateTotals( decimal subtotal, decimal tax );
    }
}
