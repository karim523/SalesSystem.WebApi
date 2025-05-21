using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Products;
using SalesSystem.Application.Products.Dtos;
using SalesSystem.Domain.Entities;

namespace SalesSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductServices _service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search(string query) =>
            Ok(await _service.SearchAsync(query));

        [HttpGet("low-stock")]
        public async Task<ActionResult> LowStock() =>
            Ok(await _service.GetLowStockAsync());

        [HttpPost]
        public async Task<ActionResult> Create(CreateProductDto productDto)
        {
            var created = await _service.AddAsync(productDto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateProductDto productDto)
        {
            var updated = await _service.UpdateAsync(id, productDto);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
