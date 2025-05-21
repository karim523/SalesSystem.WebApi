using SalesSystem.Domain.Enums;

namespace SalesSystem.Application.Invoices.Dtos
{
    public class CreateInvoiceDto
    {
        public string? CustomerName { get; set; }
        public decimal DiscountPercentage { get; set; }
        public InvoiceType Type { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }
}
