using System;
using TaxService.Interfaces;

namespace TaxService.Services
{
    public class OrderItem
        : IOrderItem
    {
        public OrderItem(string desc = "undefined", decimal price = 0, bool taxible = true )
        {
            Price = price;
            IsTaxible = taxible;
            Description = desc;
            OrderId = Guid.NewGuid();
        }

        public OrderItem( Guid id, string desc = "undefined", decimal price = 0, bool taxible = true )
            : this( desc, price, taxible )
        {
            OrderId = id;
        }

        public Guid OrderId { get; set; }
        public decimal Price { get; set; }
        public bool IsTaxible { get; set; }
        public string Description { get; set; }
    }
}
