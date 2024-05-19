using Application.Common.Interfaces.Data;
using Application.Common.Interfaces;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using Domain.Entities;
using MediatR;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Application.Orders.Commands;

namespace Application.OrderDetails.Commands;
public sealed record InsertOrderDetailsFromCsvCommand(Stream Stream) : IRequest<List<OrderDetailDto>>;

internal sealed class InsertOrderDetailsFromCsvCommandHandler : IRequestHandler<InsertOrderDetailsFromCsvCommand, List<OrderDetailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICsvImportService<OrderDetailDto, OrderDetailCsvMap> _csvImportService;
    private readonly IMediator _mediator;
    public InsertOrderDetailsFromCsvCommandHandler(
        IApplicationDbContext context,
        ICsvImportService<OrderDetailDto, OrderDetailCsvMap> csvImportService,
        IMediator mediator)
    {
        _context = context;
        _csvImportService = csvImportService;
        _mediator = mediator;
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

        foreach (var orderDetail in orderDetailDtos)
            await UpdateOrderTotal(orderDetail.OrderId);

        return orderDetailDtos;
    }

    public async Task UpdateOrderTotal(int orderId)
    {
        var totalAmount =  _context.OrderDetail
                                .Where(p => p.OrderId == orderId)
                                .Include(p => p.Pizza)
                                .Sum(p => p.Pizza.Price != null ? p.Pizza.Price : 0);
        if (totalAmount is not null)
            await _mediator.Send(new UpdateOrderTotalAmountCommand(orderId, (decimal)totalAmount));   
    }
}