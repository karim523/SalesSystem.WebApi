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
        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet("details/{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("search-by-name")]
        public async Task<ActionResult> Search([FromQuery] string query)
        {
            var result = await _mediator.Send(new SearchProductsQuery(query));
            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult> GetLowStock()
        {
            var result = await _mediator.Send(new GetLowStockProductsQuery());
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _mediator.Send(new CreateProductCommand(dto));
            return Ok(result);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, dto));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}
