using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;
using SalesSystem.Domain.Entities;

namespace SalesSystem.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var product = new Product(dto.ProductName, dto.ProductCode, dto.AvailableQuantity, dto.BuyingPrice, dto.SellingPrice, dto.ReorderLevel);

            await unitOfWork.ProductsRepository.AddProductAsync(product);
            await unitOfWork.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.Name,
                ProductCode = product.Code,
                AvailableQuantity = product.QuantityAvailable,
                BuyingPrice = product.PurchasePrice,
                SellingPrice = product.SalePrice,
                ReorderLevel = product.ReorderThreshold
            };
        }
    }
}
