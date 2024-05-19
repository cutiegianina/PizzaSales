using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderDetails.Queries;
public sealed record GetOrderDetailsQuery : IRequest<List<OrderDetailDto>>;

internal sealed class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, List<OrderDetailDto>>
{
    private readonly IApplicationDbContext _context;

    public GetOrderDetailsQueryHandler(IApplicationDbContext context) =>
        _context = context;
    public async Task<List<OrderDetailDto>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await _context.OrderDetail
                            .Include(p => p.Pizza)
                            .Include(p => p.Order)
                            .AsNoTracking()
                            .ToListAsync();
        return orderDetails.Adapt<List<OrderDetailDto>>();
    }
}