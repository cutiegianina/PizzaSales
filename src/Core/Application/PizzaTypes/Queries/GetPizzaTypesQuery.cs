using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PizzaTypes.Queries;
public sealed record GetPizzaTypesQuery : IRequest<List<PizzaTypeDto>>;

internal sealed class GetPizzaTypesQueryHandler : IRequestHandler<GetPizzaTypesQuery, List<PizzaTypeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPizzaTypesQueryHandler(IApplicationDbContext context) =>
        _context = context;

    public async Task<List<PizzaTypeDto>> Handle(GetPizzaTypesQuery request, CancellationToken cancellationToken)
    {
        var pizzaTypes = await _context.PizzaType.AsNoTracking().ToListAsync();
        return pizzaTypes.Adapt<List<PizzaTypeDto>>();
    }
}