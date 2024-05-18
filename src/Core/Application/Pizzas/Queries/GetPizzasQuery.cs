using Application.Common.Interfaces.Data;
using Application.Dtos;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Pizzas.Queries;
public sealed record GetPizzasQuery : IRequest<List<PizzaDto>>;

internal sealed class GetPizzaQueryHandler : IRequestHandler<GetPizzasQuery, List<PizzaDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPizzaQueryHandler(IApplicationDbContext context) =>
        _context = context;
    public async Task<List<PizzaDto>> Handle(GetPizzasQuery request, CancellationToken cancellationToken)
    {
        var pizzas = await _context.Pizza.AsNoTracking().ToListAsync();
        return pizzas.Adapt<List<PizzaDto>>();
    }
}