namespace SalesSystem.Domain.Entities
{
    public class Product
    {
        private const int maxValue = 100;
        public int Id { get; private set; } 
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public int QuantityAvailable { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public decimal SalePrice { get; private set; }
        public int ReorderThreshold { get; private set; }

        private Product() { }

        public Product(string name, string code, int quantityAvailable, decimal purchasePrice, decimal salePrice, int reorderThreshold)
        {
            SetName(name);
            SetCode(code);
            SetQuantity(quantityAvailable);
            SetPrice(purchasePrice, nameof(PurchasePrice));
            SetPrice(salePrice, nameof(SalePrice));
            SetReorderThreshold(reorderThreshold);
        }

        public void Update(string name, string code, int quantityAvailable, decimal purchasePrice, decimal salePrice, int reorderThreshold)
        {
            SetName(name);
            SetCode(code);
            SetQuantity(quantityAvailable);
            SetPrice(purchasePrice, nameof(PurchasePrice));
            SetPrice(salePrice, nameof(SalePrice));
            SetReorderThreshold(reorderThreshold);
           
        }

        //sale
        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            if (amount > QuantityAvailable)
                throw new InvalidOperationException("Not enough stock available.");

            QuantityAvailable -= amount;
        }
        //purchase 
        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            if (amount > maxValue - QuantityAvailable)
                throw new OverflowException("Cannot increase quantity. It would exceed the maximum allowed stock level.");

            QuantityAvailable += amount;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");
            if (name.Length > 100)
                throw new ArgumentException("Product name is too long.");
            Name = name;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Product code cannot be empty.");
            if (code.Length > 50)
                throw new ArgumentException("Product code is too long.");
            Code = code;
        }

        private void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity available cannot be negative.");
            if (quantity > maxValue)
                throw new ArgumentException($"Quantity available cannot exceed {maxValue}.");
            QuantityAvailable = quantity;
        }

        private void SetPrice(decimal price, string fieldName)
        {
            if (price < 0)
                throw new ArgumentException($"The price cannot be negative.");
            if(fieldName == nameof(PurchasePrice) )
                PurchasePrice = price;
            else if (fieldName == nameof(SalePrice))
                SalePrice = price;
        }
      
        private void SetReorderThreshold(int threshold)
        {
            if (threshold < 0)
                throw new ArgumentException("Reorder threshold cannot be negative.");
            ReorderThreshold = threshold;
        }
    }
}
