using MediatR;
using SalesSystem.Application.Invoices.Dtos;

namespace SalesSystem.Application.Invoices.Queries.SearchInvoices
{
    public record SearchInvoicesQuery(string Query) : IRequest<IEnumerable<InvoiceDto>>;

}
