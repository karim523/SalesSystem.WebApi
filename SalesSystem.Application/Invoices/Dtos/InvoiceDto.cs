using SalesSystem.Domain.Enums;

namespace SalesSystem.Application.Invoices.Dtos
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = default!;
        public InvoiceType Type { get; set; }
        public string? CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Discount => SubTotal * (DiscountPercentage / 100); 
        public decimal Total { get; set; }
    }
}
