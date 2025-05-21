using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;
using SalesSystem.Domain.Entities;
using System.Threading.Channels;

namespace SalesSystem.Application.Products
{
    public class ProductServices(IUnitOfWork _unitOfWork) : IProductServices
    {
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductsRepository.GetAllProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductCode = p.Code,
                AvailableQuantity = p.QuantityAvailable,
                BuyingPrice = p.PurchasePrice,
                SellingPrice = p.SalePrice,
                ReorderLevel = p.ReorderThreshold
            });
        }
        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be a positive number.", nameof(id));

            var product = await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.Name,
                ProductCode = product.Code,
                AvailableQuantity = product.QuantityAvailable,
                BuyingPrice = product.PurchasePrice,
                SellingPrice = product.SalePrice,
                ReorderLevel = product.ReorderThreshold
            };
        }

        public async Task<IEnumerable<ProductDto>> SearchAsync(string query)
        {
            var products = await _unitOfWork.ProductsRepository.SearchAsync(query);
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductCode = p.Code,
                AvailableQuantity = p.QuantityAvailable,
                BuyingPrice = p.PurchasePrice,
                SellingPrice = p.SalePrice,
                ReorderLevel = p.ReorderThreshold
            });
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockAsync()
        {
            var products = await _unitOfWork.ProductsRepository.GetLowStockProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductCode = p.Code,
                AvailableQuantity = p.QuantityAvailable,
                BuyingPrice = p.PurchasePrice,
                SellingPrice = p.SalePrice,
                ReorderLevel = p.ReorderThreshold
            });
        }
        public async Task<ProductDto> AddAsync(CreateProductDto dto)
        {
            var product = new Product(dto.ProductName, dto.ProductCode, dto.AvailableQuantity, dto.BuyingPrice, dto.SellingPrice, dto.ReorderLevel);
            await _unitOfWork.ProductsRepository.AddProductAsync(product);
            return (await _unitOfWork.SaveChangesAsync()) > 0
                ? new ProductDto
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    ProductCode = product.Code,
                    AvailableQuantity = product.QuantityAvailable,
                    BuyingPrice = product.PurchasePrice,
                    SellingPrice = product.SalePrice,
                    ReorderLevel = product.ReorderThreshold
                }
                : throw new InvalidOperationException("No changes were saved to the database.");
    
        }


        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be a positive number.", nameof(id));

            var existingProduct = await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);
            if (existingProduct is null) return null;

            existingProduct.Update(dto.ProductName, dto.ProductCode, dto.AvailableQuantity, dto.BuyingPrice, dto.SellingPrice, dto.ReorderLevel);

            await _unitOfWork.ProductsRepository.UpdateProductAsync(existingProduct);
            return (await _unitOfWork.SaveChangesAsync()) > 0
                 ? new ProductDto
                 {
                     Id = existingProduct.Id,
                     ProductName = existingProduct.Name,
                     ProductCode = existingProduct.Code,
                     AvailableQuantity = existingProduct.QuantityAvailable,
                     BuyingPrice = existingProduct.PurchasePrice,
                     SellingPrice = existingProduct.SalePrice,
                     ReorderLevel = existingProduct.ReorderThreshold
                 }
                 : throw new InvalidOperationException("No changes were saved to the database.");

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _unitOfWork.ProductsRepository.GetProductByIdAsync(id);

            if (existing is null)
                  throw new Exception($"Product with ID {id} not found");
            bool isUsedInInvoices = await _unitOfWork.InvoicesRepository
               .AnyAsync(id);

            if (isUsedInInvoices)
                throw new InvalidOperationException("Cannot delete product because it is used in invoices.");
            await _unitOfWork.ProductsRepository.DeleteProductAsync(existing);
            return (await _unitOfWork.SaveChangesAsync()) > 0
                ? true
                : throw new InvalidOperationException("No changes were saved to the database.");
        }
    }
}
