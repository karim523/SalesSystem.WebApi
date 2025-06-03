using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Application.Invoices.Commands.CreateInvoice;
using SalesSystem.Application.Invoices.Dtos;
using SalesSystem.Application.Invoices.Queries.GetAllInvoices;
using SalesSystem.Application.Invoices.Queries.GetInvoiceById;
using SalesSystem.Application.Invoices.Queries.SearchInvoices;
using SalesSystem.Domain.ErrorModels;
using System.Net;

namespace SalesSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class InvoicesController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetInvoiceByIdQuery(id));
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _mediator.Send(new CreateInvoiceCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.InvoiceId }, result);
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery] string query)
        {
            var result = await _mediator.Send(new SearchInvoicesQuery(query));
            return Ok(result);
        }

    }
}
