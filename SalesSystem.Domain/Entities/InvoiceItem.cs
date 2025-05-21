namespace SalesSystem.Domain.Entities
{
    public class InvoiceItem
    {
        public int Id { get; private set; }
        public Invoice Invoice { get; private set; } = null!;
        public int InvoiceId { get; private set; }
        public Product Product { get; private set; } = null!;
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        private InvoiceItem()
        {

        }
        public InvoiceItem(int productId, int quantity, decimal unitPrice)
        {
            SetProductId(productId);
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
        }

        private void SetProductId(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("Product ID must be a positive number.");
            ProductId = productId;
        }

        private void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity must be a positive number.");
            Quantity = quantity;
        }

        private void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");
            UnitPrice = unitPrice;
        }
    }
}
