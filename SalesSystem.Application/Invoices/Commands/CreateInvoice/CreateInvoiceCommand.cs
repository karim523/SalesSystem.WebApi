using MediatR;
using SalesSystem.Application.Invoices.Dtos;

namespace SalesSystem.Application.Invoices.Commands.CreateInvoice
{
    public record CreateInvoiceCommand(CreateInvoiceDto InvoiceDto) : IRequest<InvoiceDto>;
}
