using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductCommand, ProductDto?>
    {
        public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await unitOfWork.ProductsRepository.GetProductByIdAsync(request.Id);
            if (existingProduct is null) return null;

            existingProduct.Update(
                request.Dto.ProductName,
                request.Dto.ProductCode,
                request.Dto.AvailableQuantity,
                request.Dto.BuyingPrice,
                request.Dto.SellingPrice,
                request.Dto.ReorderLevel
            );

            await unitOfWork.ProductsRepository.UpdateProductAsync(existingProduct);
            await unitOfWork.SaveChangesAsync();

            return new ProductDto
            {
                Id = existingProduct.Id,
                ProductName = existingProduct.Name,
                ProductCode = existingProduct.Code,
                AvailableQuantity = existingProduct.QuantityAvailable,
                BuyingPrice = existingProduct.PurchasePrice,
                SellingPrice = existingProduct.SalePrice,
                ReorderLevel = existingProduct.ReorderThreshold
            };
        }
    }
}
