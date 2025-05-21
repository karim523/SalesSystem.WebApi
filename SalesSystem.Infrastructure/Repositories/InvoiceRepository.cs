using SalesSystem.Domain.IRepositories;

namespace SalesSystem.Infrastructure.Repositories
{
    public class InvoiceRepository(AppDbContext _context) : IInvoiceRepository
    {
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> SearchAsync(string query)
        {
            return await _context.Invoices
                .Where(i => i.InvoiceNumber.ToLower().Contains(query.ToLower()) ||
                (i.CustomerName != null && i.CustomerName.ToLower().Contains(query.ToLower())))
                .Include(i => i.Items)
                .ToListAsync();
        }

        public async Task AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
        }

        public  Task<bool> AnyAsync(int productId)=>
            _context.Invoices.AnyAsync(i=>i.Items.Any(x=>x.ProductId==productId));
    }
}
