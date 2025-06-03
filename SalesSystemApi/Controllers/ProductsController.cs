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






        //[HttpGet]
        //public async Task<ActionResult> GetAll() => Ok(await _service.GetAllAsync());

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetById(int id)
        //{
        //    var product = await _service.GetByIdAsync(id);
        //    return product is null ? NotFound() : Ok(product);
        //}

        //[HttpGet("search")]
        //public async Task<ActionResult> Search(string query) =>
        //    Ok(await _service.SearchAsync(query));

        //[HttpGet("low-stock")]
        //public async Task<ActionResult> LowStock() =>
        //    Ok(await _service.GetLowStockAsync());

        //[HttpPost]
        //public async Task<ActionResult> Create(CreateProductDto productDto)
        //{
        //    var created = await _service.AddAsync(productDto);
        //    return Ok(created);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, UpdateProductDto productDto)
        //{
        //    var updated = await _service.UpdateAsync(id, productDto);
        //    return updated is null ? NotFound() : Ok(updated);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var deleted = await _service.DeleteAsync(id);
        //    return deleted ? NoContent() : NotFound();
        //}
    }
}
