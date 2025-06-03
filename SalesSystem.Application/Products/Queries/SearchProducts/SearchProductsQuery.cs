using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Queries.SearchProducts
{
    public record SearchProductsQuery(string Query) : IRequest<IEnumerable<ProductDto>>;

}
