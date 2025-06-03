using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;

}
