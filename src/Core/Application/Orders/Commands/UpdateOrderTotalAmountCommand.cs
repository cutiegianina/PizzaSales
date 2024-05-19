using Application.Common.Interfaces.Data;
using MediatR;

namespace Application.Orders.Commands;

public sealed record UpdateOrderTotalAmountCommand(int OrderId,  decimal TotalAmount) : IRequest<int>;

internal sealed class UpdateOrderTotalAmountCommandHandler : IRequestHandler<UpdateOrderTotalAmountCommand, int>
{
    private readonly IApplicationDbContext _context;
    public UpdateOrderTotalAmountCommandHandler(IApplicationDbContext context) =>
        _context = context;
    public async Task<int> Handle(UpdateOrderTotalAmountCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Order.FindAsync(request.OrderId, cancellationToken);
        if (order is null)
            return 0;

        order.TotalAmount = request.TotalAmount;
        _context.Order.Update(order);
        return await _context.SaveChangesAsync();
    }
}