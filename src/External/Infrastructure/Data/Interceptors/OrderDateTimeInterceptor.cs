using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;
public class OrderDateTimeInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);


        var entries = dbContext.ChangeTracker.Entries<OrderDateTime>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.OrderDate).CurrentValue = DateTime.UtcNow;
                entry.Property(x => x.OrderTime).CurrentValue = DateTime.UtcNow.TimeOfDay;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}