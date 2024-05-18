using Application.Common.Interfaces.Data;
using Application.Common.Interfaces;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using Domain.Entities;
using MediatR;
using Mapster;

namespace Application.OrderDetails.Commands;
public sealed record InsertOrderDetailsFromCsvCommand(Stream Stream) : IRequest<List<OrderDetailDto>>;

internal sealed class InsertOrderDetailsFromCsvCommandHandler : IRequestHandler<InsertOrderDetailsFromCsvCommand, List<OrderDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICsvImportService<OrderDetailDto, OrderDetailCsvMap> _csvImportService;
    public InsertOrderDetailsFromCsvCommandHandler(
        IApplicationDbContext context,
        ICsvImportService<OrderDetailDto, OrderDetailCsvMap> csvImportService)
    {
        _context = context;
        _csvImportService = csvImportService;
    }
    public async Task<List<OrderDetailDto>> Handle(InsertOrderDetailsFromCsvCommand request, CancellationToken cancellationToken)
    {
        var orderDetailDtos = await _csvImportService.ImportCsvAsync(request.Stream);

        using var transaction = await _context.BeginTransactionAsync(cancellationToken);
        await _context.ExecuteSqlRawAsync("SET IDENTITY_INSERT OrderDetails ON");

        var orderDetails = orderDetailDtos.Adapt<List<OrderDetail>>();
        await _context.OrderDetail.AddRangeAsync(orderDetails, cancellationToken);
        var insertedData = await _context.SaveChangesAsync(cancellationToken);

        await _context.ExecuteSqlRawAsync("SET IDENTITY_INSERT OrderDetails OFF");
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return orderDetailDtos;
    }
}