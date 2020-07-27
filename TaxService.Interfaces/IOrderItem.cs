using System;

namespace TaxService.Interfaces
{
    public interface IOrderItem
    {
        Guid OrderId { get; set; }
        decimal Price { get; set; }
        bool IsTaxible { get; set; }
        string Description { get; set; }
    }
}
