using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Products.Queries.GetProductById
{
    public class GetProductByIdHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.ProductsRepository.GetProductByIdAsync(request.Id);
            if (product is null) return null;

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
    }
}
