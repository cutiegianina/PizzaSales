using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Queries;

public sealed record GetOrdersQuery : IRequest<List<OrderDto>>;

internal sealed class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context) =>
        _context = context;
    public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _context.Order
                        .Include(p => p.Customer)
                        .Include(p => p.PaymentMethod)
                        .AsNoTracking()
                        .ToListAsync();
        return orders.Adapt<List<OrderDto>>();
    }
}