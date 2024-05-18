using Application.Dtos;
using Application.Pizzas.Commands;
using Application.Pizzas.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzaController : ControllerBase
{
    private readonly IMediator _mediator;

    public PizzaController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<PizzaDto>>> Get()
    {
        var pizzas = await _mediator.Send(new GetPizzasQuery());
        if (pizzas is null)
            return NotFound();

        return Ok(pizzas);
    }

    [HttpPost("import-csv")]
    public async Task<ActionResult<List<PizzaDto>>> ImportCsv([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("No file uploaded!");

        using var stream = file.OpenReadStream();
        var pizzas = await _mediator.Send(new InsertPizzasFromCsvCommand(stream));
        if (pizzas.Count <= 0)
            return NoContent();

        return CreatedAtAction(nameof(Get), new {}, pizzas);
    }
}