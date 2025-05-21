using SalesSystem.Domain;
using SalesSystem.Domain.IRepositories;

namespace SalesSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, IProductRepository productRepository,
                          IInvoiceRepository invoiceRepository)
        {
            _context = context;
            ProductsRepository = productRepository;
            InvoicesRepository = invoiceRepository;
        }

        public IProductRepository ProductsRepository { get; }

        public IInvoiceRepository InvoicesRepository { get; }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
