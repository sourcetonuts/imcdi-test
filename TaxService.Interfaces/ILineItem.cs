namespace TaxService.Interfaces
{
    public interface ILineItem
    {
        string Description { get; }
        decimal Quantity { get; set; }
        decimal Price { get; }
        bool IsTaxible { get; }
    }
}
