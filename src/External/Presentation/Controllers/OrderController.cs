using Application.Dtos;
using Application.Orders.Commands;
using Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> Get()
    {
        var orders = await _mediator.Send(new GetOrdersQuery());
        if (orders is null)
            return NotFound();

        return Ok(orders);
    }

    [HttpPost("import-csv")]
    public async Task<ActionResult<List<OrderDto>>> ImportCsv([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded!");

        using var stream = file.OpenReadStream();
        var orders = await _mediator.Send(new InsertOrdersFromCsvCommand(stream));
        if (orders.Count <= 0)
            return NoContent();

        var firstId = orders.First().OrderId;
        return CreatedAtAction(nameof(Get), new {}, orders);
    }
}