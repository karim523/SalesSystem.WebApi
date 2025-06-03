using MediatR;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Invoices.Queries.GetInvoiceById
{
    public class GetInvoiceByIdHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto?>
    {
        public async Task<InvoiceDto?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.Id);
            if (invoice is null) throw new ArgumentException($"Invoice with ID {request.Id} not found");

            return new InvoiceDto
            {
                InvoiceId = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                CustomerName = invoice.CustomerName,
                InvoiceDate = invoice.Date,
                SubTotal = invoice.SubTotal,
                DiscountPercentage = invoice.DiscountPercentage,
                Total = invoice.TotalAmount,
                Type = invoice.Type
            };
        }
    }

}
