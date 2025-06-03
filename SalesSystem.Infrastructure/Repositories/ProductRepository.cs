using SalesSystem.Domain.IRepositories;

namespace SalesSystem.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {
        public async Task AddProductAsync(Product product)=>
                 await _context.Products.AddAsync(product);


        public Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _context.Products.AsNoTracking().ToListAsync(); 

        public async Task<List<Product>> GetLowStockProductsAsync() =>
            await _context.Products.AsNoTracking()
                .Where(p => p.QuantityAvailable < p.ReorderThreshold)
                .ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> SearchAsync(string query) =>
            await _context.Products
                .AsNoTracking()
                .Where(p => p.Name.ToLower().Contains(query.ToLower().Trim()) ||
                       p.Code.ToLower().Contains(query.ToLower().Trim()))
                .ToListAsync();

        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }
    }
}
