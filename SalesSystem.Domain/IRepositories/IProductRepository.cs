global using SalesSystem.Domain.Entities;

namespace SalesSystem.Domain.IRepositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<List<Product>> GetLowStockProductsAsync();
        Task<IEnumerable<Product>> SearchAsync(string query);

    }
}
