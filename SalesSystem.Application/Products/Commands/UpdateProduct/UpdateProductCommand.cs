using MediatR;
using SalesSystem.Application.Products.Dtos;

namespace SalesSystem.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(int Id, UpdateProductDto Dto) : IRequest<ProductDto?>;

}
