using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Invoices.Commands.CreateInvoice;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Application.Invoices.Queries.GetAllInvoices;
using SalesSystem.Application.Invoices.Queries.GetInvoiceById;
using SalesSystem.Application.Invoices.Queries.SearchInvoices;

namespace SalesSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(result);
        }

        // GET: api/invoices/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetInvoiceByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _mediator.Send(new CreateInvoiceCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.InvoiceId }, result);
        }

        // GET: api/invoices/search?query=value
        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery] string query)
        {
            var result = await _mediator.Send(new SearchInvoicesQuery(query));
            return Ok(result);
        }




        //[HttpGet]
        //public async Task<ActionResult> GetAll()
        //{
        //    var result = await _invoiceService.GetAllAsync();
        //    return Ok(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetById(int id)
        //{
        //    var invoice = await _invoiceService.GetByIdAsync(id);
        //    if (invoice == null) return NotFound();
        //    return Ok(invoice);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Create(CreateInvoiceDto dto)
        //{
        //    var result = await _invoiceService.CreateAsync(dto);
        //    return Ok(result);
        //}

        //[HttpGet("search")]
        //public async Task<ActionResult> Search(string query)
        //{
        //    var result = await _invoiceService.SearchAsync(query);
        //    return Ok(result);
        //}



    }
}
