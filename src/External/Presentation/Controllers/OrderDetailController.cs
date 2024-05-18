using Application.Dtos;
using Application.OrderDetails.Commands;
using Application.OrderDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderDetailController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<OrderDetailDto>>> Get()
    {
        var orders = await _mediator.Send(new GetOrderDetailsQuery());
        if (orders is null)
            return NotFound();

        return Ok(orders);
    }

    [HttpPost("import-csv")]
    public async Task<ActionResult<List<OrderDetailDto>>> ImportCsv([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded!");

        using var stream = file.OpenReadStream();
        var orderDetails = await _mediator.Send(new InsertOrderDetailsFromCsvCommand(stream));
        if (orderDetails.Count <= 0)
            return NoContent();

        return CreatedAtAction(nameof(Get), new {}, orderDetails);
    }
}