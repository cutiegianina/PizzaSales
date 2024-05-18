using Application.OrderDetails.Commands;
using Application.Orders.Commands;
using Application.Pizzas.Commands;
using Application.PizzaTypes.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        if (files is null || files.Length == 0)
            return BadRequest("No file uploaded!");

        int insertedData = 0;
        // Iterate through the request body which may contain mutiple CSVs.
        foreach (IFormFile file in files)
        {
            using var stream = file.OpenReadStream();
            string fileName = file.FileName.Split(".")[0];

            // Send CSV file to its respective command handler depending on its filename.
            switch (fileName.ToLower())
            {
                case "pizza":
                    var pizzas = await _mediator.Send(new InsertPizzasFromCsvCommand(stream));
                    insertedData += pizzas.Count;
                    break;
                case "pizza_types":
                    var pizzaTypes = await _mediator.Send(new InsertPizzaTypesFromCsvCommand(stream));
                    insertedData += pizzaTypes.Count;
                    break;
                case "orders":
                    var orders = await _mediator.Send(new InsertOrdersFromCsvCommand(stream));
                    insertedData += orders.Count;
                    break;
                case "order_details":
                    var orderDetails = await _mediator.Send(new InsertOrderDetailsFromCsvCommand(stream));
                    insertedData += orderDetails.Count;
                    break;
                default:
                    break;
            };
        }

        var httpResponse = new
        {
            StatusCode = insertedData > 0 ? (int)HttpStatusCode.Created : (int)HttpStatusCode.NoContent,
            Method = "POST",
            Count = insertedData,
            Message = insertedData > 0 ? "Data imported successfully!" : "No file uploaded!"
        };
        return insertedData > 0 ? 
            StatusCode((int)HttpStatusCode.Created, httpResponse) : 
            StatusCode((int)HttpStatusCode.NoContent, httpResponse);
    }
}