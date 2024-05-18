using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Data;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<Pizza> Pizza { get; set; }
    DbSet<PizzaType> PizzaType { get; set;}
    DbSet<Order> Order { get; set; }
    DbSet<OrderDetail> OrderDetail { get; set; }
}