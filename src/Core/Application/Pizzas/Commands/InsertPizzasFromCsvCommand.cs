using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Pizzas.Commands;
public sealed record InsertPizzasFromCsvCommand(Stream Stream) : IRequest<List<PizzaDto>>;

internal sealed class InsertPizzasFromCsvCommandHandler : IRequestHandler<InsertPizzasFromCsvCommand, List<PizzaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICsvImportService<PizzaDto, PizzaCsvMap> _csvImportService;
    public InsertPizzasFromCsvCommandHandler(
        IApplicationDbContext context,
        ICsvImportService<PizzaDto, PizzaCsvMap> csvImportService)
    {
        _context = context;
        _csvImportService = csvImportService;
    }
    public async Task<List<PizzaDto>> Handle(InsertPizzasFromCsvCommand request, CancellationToken cancellationToken)
    {
        var pizzaDtos = await _csvImportService.ImportCsvAsync(request.Stream);
        var pizzas = pizzaDtos.Adapt<List<Pizza>>();
        await _context.Pizza.AddRangeAsync(pizzas, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return pizzaDtos;
    }
}