using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Products.Queries.SearchProducts
{
    public class SearchProductsHandler(IUnitOfWork unitOfWork) : IRequestHandler<SearchProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.ProductsRepository.SearchAsync(request.Query);
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
