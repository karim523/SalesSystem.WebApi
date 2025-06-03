using MediatR;
using SalesSystem.Application.Invoices.Dtos;

namespace SalesSystem.Application.Invoices.Queries.GetAllInvoices
{
    public record GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>;

}
