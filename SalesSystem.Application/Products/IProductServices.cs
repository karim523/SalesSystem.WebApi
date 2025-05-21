global using SalesSystem.Domain.Entities;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> SearchAsync(string query);
        Task<IEnumerable<ProductDto>> GetLowStockAsync();
        Task<ProductDto> AddAsync(CreateProductDto product);
        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto product);
        Task<bool> DeleteAsync(int id);
    }
}
