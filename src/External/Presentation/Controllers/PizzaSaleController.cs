using Application.OrderDetails.Commands;
using Application.Orders.Commands;
using Application.Pizzas.Commands;
using Application.PizzaTypes.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PizzaSaleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PizzaSaleController(IMediator mediator) =>
        _mediator = mediator;

    [HttpPost("import-csv")]
    public async Task<IActionResult> ImportCsv([FromForm] IFormFile[] files)
    {
        if (files == null || files.Length == 0)
            return BadRequest("No file uploaded!");

        int insertedData = 0;

        foreach (IFormFile file in files)
        {
            using var stream = file.OpenReadStream();
            string fileName = file.FileName.Split(".")[0];

            insertedData += fileName switch
            {
                "pizzas"            => await _mediator.Send(new InsertPizzasFromCsvCommand(stream)),
                "pizza_types"       => await _mediator.Send(new InsertPizzaTypesFromCsvCommand(stream)),
                "orders(edited)"            => await _mediator.Send(new InsertOrdersFromCsvCommand(stream)),
                "order_details"     => await _mediator.Send(new InsertOrderDetailsFromCsvCommand(stream)),
                _                   => 0
            };
        }
        return insertedData > 0 ? Ok($"Data imported successfully! {insertedData} data inserted.") : BadRequest("No file uploaded!");
    }

}