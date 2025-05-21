using SalesSystem.Application.Invoices.Dtos;

namespace SalesSystem.Application.Invoices
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto?> GetByIdAsync(int id);
        Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto);
        Task<IEnumerable<InvoiceDto>> SearchAsync(string query);
    }
}
