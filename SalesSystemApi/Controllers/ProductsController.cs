using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Products.Commands.CreateProduct;
using SalesSystem.Application.Products.Commands.DeleteProduct;
using SalesSystem.Application.Products.Commands.UpdateProduct;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Application.Products.Queries.GetAllProducts;
using SalesSystem.Application.Products.Queries.GetLowStockProducts;
using SalesSystem.Application.Products.Queries.GetProductById;
using SalesSystem.Application.Products.Queries.SearchProducts;
using SalesSystem.Domain.ErrorModels;
using System.Net;

namespace SalesSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class ProductsController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _mediator.Send(new SearchProductsQuery(query));
            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var result = await _mediator.Send(new GetLowStockProductsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(dto));
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, dto));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}
