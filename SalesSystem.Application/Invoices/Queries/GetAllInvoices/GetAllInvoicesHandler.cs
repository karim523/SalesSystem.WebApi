using MediatR;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _unitOfWork.InvoicesRepository.GetAllAsync();
            return invoices.Select(i => new InvoiceDto
            {
                InvoiceId = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                CustomerName = i.CustomerName,
                InvoiceDate = i.Date,
                SubTotal = i.SubTotal,
                DiscountPercentage = i.DiscountPercentage,
                Total = i.TotalAmount,
                Type = i.Type
            });
        }
    }
}
