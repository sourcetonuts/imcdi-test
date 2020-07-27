using System.Diagnostics;
using TaxService.Interfaces;

namespace TaxService.Services
{
    public class LineItem
        : ILineItem
    {
        private decimal _quantity = 0;
        private readonly IOrderItem _item;

        public LineItem( IOrderItem item, decimal quantity = 1 )
        {
            _item = item;
            Debug.Assert(_item != null);
            Quantity = quantity;
        }

        public string Description 
        {
            get { return _item.Description; }
        }

        public bool IsTaxible
        {
            get { return _item.IsTaxible; }
        }
        
        public decimal Quantity 
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                UpdatePrice();
            }
        }

        public decimal Price { get; protected set; }
    
        protected void UpdatePrice()
        {
            Price = _item.Price * Quantity;
        }
    }
}
