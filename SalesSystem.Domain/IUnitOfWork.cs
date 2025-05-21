using SalesSystem.Domain.IRepositories;

namespace SalesSystem.Domain
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IProductRepository ProductsRepository { get; }
        IInvoiceRepository InvoicesRepository { get; }
    }
}
