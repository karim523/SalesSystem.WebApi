using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain;
using SalesSystem.Domain.Enums;

namespace SalesSystem.Application.Invoices
{
    public class InvoiceService(IUnitOfWork _unitOfWork) : IInvoiceService
    {
        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
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

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(id);
            if (invoice is null) return null;

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

        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice(dto.CustomerName, DateTime.UtcNow, dto.DiscountPercentage, dto.Type);

            foreach (var item in dto.Items)
            {
                var product = await _unitOfWork.ProductsRepository.GetProductByIdAsync(item.ProductId);
                if (product is null) throw new Exception($"Product with ID {item.ProductId} not found");

                if (dto.Type == InvoiceType.Sale)
                {
                    if (product.QuantityAvailable < item.Quantity)
                        throw new Exception($"Not enough stock for product {product.Name}");

                    product.DecreaseQuantity(item.Quantity);
                }

                else if (dto.Type == InvoiceType.Purchase)
                    product.IncreaseQuantity(item.Quantity);
                

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

        public async Task<IEnumerable<InvoiceDto>> SearchAsync(string query)
        {
            var invoices = await _unitOfWork.InvoicesRepository.SearchAsync(query);
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
