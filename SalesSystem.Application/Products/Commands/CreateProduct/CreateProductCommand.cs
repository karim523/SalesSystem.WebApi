using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand(CreateProductDto Dto) : IRequest<ProductDto>;

}
