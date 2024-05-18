using Application.Common.Interfaces.Data;
using Application.Common.Interfaces;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using MediatR;
using Domain.Entities;
using Mapster;

namespace Application.PizzaTypes.Commands;
public sealed record InsertPizzaTypesFromCsvCommand(Stream Stream) : IRequest<int>;

internal sealed class InsertPizzaTypesFromCsvCommandHandler : IRequestHandler<InsertPizzaTypesFromCsvCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICsvImportService<PizzaTypeDto, PizzaTypeCsvMap> _csvImportService;
    public InsertPizzaTypesFromCsvCommandHandler(
        IApplicationDbContext context,
        ICsvImportService<PizzaTypeDto, PizzaTypeCsvMap> csvImportService)
    {
        _context = context;
        _csvImportService = csvImportService;
    }
    public async Task<int> Handle(InsertPizzaTypesFromCsvCommand request, CancellationToken cancellationToken)
    {
        var pizzaTypesDtos = await _csvImportService.ImportCsvAsync(request.Stream);
        var pizzaTypes = pizzaTypesDtos.Adapt<List<PizzaType>>();
        await _context.PizzaType.AddRangeAsync(pizzaTypes, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}