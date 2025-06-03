using MediatR;

namespace SalesSystem.Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand(int Id) : IRequest<bool>;

}
