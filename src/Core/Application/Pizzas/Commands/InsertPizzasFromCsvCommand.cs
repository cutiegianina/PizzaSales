using Application.Common.Interfaces;
using Application.Common.Interfaces.Data;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Pizzas.Commands;
public sealed record InsertPizzasFromCsvCommand(Stream Stream) : IRequest<int>;

internal sealed class InsertPizzasFromCsvCommandHandler : IRequestHandler<InsertPizzasFromCsvCommand, int>
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
    public async Task<int> Handle(InsertPizzasFromCsvCommand request, CancellationToken cancellationToken)
    {
        var pizzaDtos = await _csvImportService.ImportCsvAsync(request.Stream);
        var pizzas = pizzaDtos.Adapt<List<Pizza>>();
        await _context.Pizza.AddRangeAsync(pizzas, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}