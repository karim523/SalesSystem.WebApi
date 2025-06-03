using MediatR;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain;
using SalesSystem.Domain.Entities;
using SalesSystem.Domain.Enums;

namespace SalesSystem.Application.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
    {
        public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var dto = request.InvoiceDto;
            if (dto.Items == null || !dto.Items.Any())
                throw new ArgumentException("Invoice must contain at least one item.");

            var invoice = new Invoice(dto.CustomerName, DateTime.UtcNow, dto.DiscountPercentage, dto.Type);

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.ProductsRepository.GetProductByIdAsync(item.ProductId);
                if (product is null)
                    throw new Exception($"Product with ID {item.ProductId} not found");

                if (dto.Type == InvoiceType.Sale)
                {
                    if (product.QuantityAvailable < item.Quantity)
                        throw new Exception($"Not enough stock for product {product.Name}");

                    product.DecreaseQuantity(item.Quantity);
                }
                else if (dto.Type == InvoiceType.Purchase)
                {
                    product.IncreaseQuantity(item.Quantity);
                }

                await _unitOfWork.ProductsRepository.UpdateProductAsync(product);

                var invoiceItem = new InvoiceItem(product.Id, item.Quantity, product.SalePrice);
                invoice.AddItem(invoiceItem);
            }

            await _unitOfWork.InvoicesRepository.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

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
