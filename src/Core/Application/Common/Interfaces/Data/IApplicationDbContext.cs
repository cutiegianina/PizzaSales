using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces.Data;
public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task ExecuteSqlRawAsync(string sql);
    DbSet<Pizza> Pizza { get; set; }
    DbSet<PizzaType> PizzaType { get; set;}
    DbSet<Order> Order { get; set; }
    DbSet<OrderDetail> OrderDetail { get; set; }
    DbSet<Customer> Customer { get; set; }
}