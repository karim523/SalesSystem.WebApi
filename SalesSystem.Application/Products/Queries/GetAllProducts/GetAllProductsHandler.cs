using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.ProductsRepository.GetAllProductsAsync();
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
    }
}
