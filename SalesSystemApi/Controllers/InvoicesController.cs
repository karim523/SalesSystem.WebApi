using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Invoices;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Domain.ErrorModels;
using System.Net;

namespace SalesSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IInvoiceService _invoiceService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _invoiceService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateInvoiceDto dto)
        {
            var result = await _invoiceService.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search(string query)
        {
            var result = await _invoiceService.SearchAsync(query);
            return Ok(result);
        }
    }
}
