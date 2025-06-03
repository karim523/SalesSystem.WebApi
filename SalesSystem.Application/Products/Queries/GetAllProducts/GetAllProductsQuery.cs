using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

}
