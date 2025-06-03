using MediatR;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain;

namespace SalesSystem.Application.Invoices.Queries.SearchInvoices
{
    public class SearchInvoicesHandler(IUnitOfWork _unitOfWork) : IRequestHandler<SearchInvoicesQuery, IEnumerable<InvoiceDto>>
    {
        public async Task<IEnumerable<InvoiceDto>> Handle(SearchInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _unitOfWork.InvoicesRepository.SearchAsync(request.Query);
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
