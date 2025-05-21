
namespace SalesSystem.Domain.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> SearchAsync(string query);
        Task AddAsync(Invoice invoice);
        Task<bool> AnyAsync(int productId); 
    }
}
