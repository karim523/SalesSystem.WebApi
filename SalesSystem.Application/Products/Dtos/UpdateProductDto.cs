namespace SalesSystem.Application.Products.Dtos
{
    public class UpdateProductDto
    {
        public string ProductName { get; set; } = default!;
        public string ProductCode { get; set; } = default!;
        public int AvailableQuantity { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReorderLevel { get; set; }
    }
}
