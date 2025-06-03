using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Queries.GetLowStockProducts
{
    public record GetLowStockProductsQuery() : IRequest<IEnumerable<ProductDto>>;

}
