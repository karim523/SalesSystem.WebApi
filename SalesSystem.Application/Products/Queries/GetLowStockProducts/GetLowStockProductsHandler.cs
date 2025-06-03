using MediatR;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Products.Queries.GetLowStockProducts
{
    public class GetLowStockProductsHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetLowStockProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetLowStockProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.ProductsRepository.GetLowStockProductsAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductCode = p.Code,
                AvailableQuantity = p.QuantityAvailable,
                BuyingPrice = p.PurchasePrice,
                SellingPrice = p.SalePrice,
                ReorderLevel = p.ReorderThreshold
            });
        }
    }

}
