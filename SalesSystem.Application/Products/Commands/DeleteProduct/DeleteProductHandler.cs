using MediatR;
using SalesSystem.Domain;

namespace SalesSystem.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existing = await unitOfWork.ProductsRepository.GetProductByIdAsync(request.Id);
            if (existing is null) throw new Exception($"Product with ID {request.Id} not found");

            var isUsedInInvoices = await unitOfWork.InvoicesRepository.AnyAsync(request.Id);
            if (isUsedInInvoices)
                throw new InvalidOperationException("Cannot delete product because it is used in invoices.");

            await unitOfWork.ProductsRepository.DeleteProductAsync(existing);
            return await unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
