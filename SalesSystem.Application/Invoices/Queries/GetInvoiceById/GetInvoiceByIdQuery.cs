using MediatR;
using SalesSystem.Application.Invoices.Dtos;

namespace SalesSystem.Application.Invoices.Queries.GetInvoiceById
{
    public record GetInvoiceByIdQuery(int Id) : IRequest<InvoiceDto?>;

}
