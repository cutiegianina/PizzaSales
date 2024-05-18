using Application.Dtos;
using Application.PizzaTypes.Commands;
using Application.PizzaTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzaTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public PizzaTypeController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<PizzaTypeDto>>> Get()
    {
        var pizzaTypes = await _mediator.Send(new GetPizzaTypesQuery());
        if (pizzaTypes is null)
            return NotFound();

        return Ok(pizzaTypes);
    }

    [HttpPost("import-csv")]
    public async Task<ActionResult<List<PizzaTypeDto>>> ImportCsv([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded!");

        using var stream = file.OpenReadStream();
        var pizzaTypes = await _mediator.Send(new InsertPizzaTypesFromCsvCommand(stream));
        if (pizzaTypes.Count <= 0)
            return NoContent();

        return CreatedAtAction(nameof(Get), new {}, pizzaTypes);
    }
}