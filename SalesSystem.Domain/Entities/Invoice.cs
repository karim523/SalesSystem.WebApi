using SalesSystem.Domain.Enums;

namespace SalesSystem.Domain.Entities
{
    public class Invoice
    {
        private readonly List<InvoiceItem> _items = new();

        public int Id { get; private set; }
        public string InvoiceNumber { get; private set; } = default!;
        public string? CustomerName { get; private set; }
        public DateTime Date { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal DiscountPercentage { get; private set; }  
        public decimal Discount => SubTotal * (DiscountPercentage / 100);
        public InvoiceType Type { get; private set; }
        public decimal TotalAmount => SubTotal - Discount;
        public IReadOnlyCollection<InvoiceItem> Items => this._items.AsReadOnly();

        private Invoice()
        {

        }
   
        public Invoice(string? customerName, DateTime date, decimal discountPercentage, InvoiceType type)
        {
            SetCustomerName(customerName);
            SetDate(date);
            SetDiscountPercentage(discountPercentage);
            SetType(type);
            SubTotal = 0m;
            InvoiceNumber = GenerateInvoiceNumber();
        }

        public void AddItem(InvoiceItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _items.Add(item);
            SubTotal += item.TotalPrice;
        }
        private string GenerateInvoiceNumber()
        {
            return $"INV{DateTime.UtcNow:yyyyMMddHHmmssfff}"; 
        }
        private void SetCustomerName(string? name)
        {
            if (name?.Length > 100)
                throw new ArgumentException("Customer name is too long.");
            CustomerName = name;
        }

        private void SetDate(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Date must be a valid value.");
            if (date > DateTime.UtcNow)
                throw new ArgumentException("Date cannot be in the future.");
            Date = date;

        }

        private void SetDiscountPercentage(decimal discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100.");
            DiscountPercentage = discountPercentage;
        }
        private void SetType(InvoiceType type)
        {
            if (type != InvoiceType.Sale && type != InvoiceType.Purchase)
                throw new ArgumentException("Only Sale and Purchase types are allowed.", nameof(type));

            Type = type;
        }
    }
}
