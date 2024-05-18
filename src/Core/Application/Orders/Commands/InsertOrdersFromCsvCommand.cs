using Application.Common.Interfaces.Data;
using Application.Common.Interfaces;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using MediatR;
using Domain.Entities;
using Mapster;

namespace Application.Orders.Commands;
public sealed record InsertOrdersFromCsvCommand(Stream Stream) : IRequest<int>;

internal sealed class InsertOrdersFromCsvCommandHandler : IRequestHandler<InsertOrdersFromCsvCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICsvImportService<OrderDto, OrderCsvMap> _csvImportService;
    public InsertOrdersFromCsvCommandHandler(
        IApplicationDbContext context,
        ICsvImportService<OrderDto, OrderCsvMap> csvImportService)
    {
        _context = context;
        _csvImportService = csvImportService;
    }
    public async Task<int> Handle(InsertOrdersFromCsvCommand request, CancellationToken cancellationToken)
    {
        var orderDtos = await _csvImportService.ImportCsvAsync(request.Stream);

        using var transaction = await _context.BeginTransactionAsync(cancellationToken);
        await _context.ExecuteSqlRawAsync("SET IDENTITY_INSERT Orders ON");

        var orders = orderDtos.Adapt<List<Order>>();
        await _context.Order.AddRangeAsync(orders, cancellationToken);
        var insertedData =  await _context.SaveChangesAsync(cancellationToken);

        await _context.ExecuteSqlRawAsync("SET IDENTITY_INSERT Orders OFF");
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return insertedData;
    }
}