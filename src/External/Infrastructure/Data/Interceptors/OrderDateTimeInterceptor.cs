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
            var orderDateCurrentValue = entry.Property(x => x.OrderDate);
            var orderTimeCurrentValue = entry.Property(x => x.OrderTime);

            if (entry.State == EntityState.Added)
            {
                if (orderDateCurrentValue.CurrentValue == DateTime.MinValue)
                    orderDateCurrentValue.CurrentValue = DateTime.Today;

                if (orderTimeCurrentValue.CurrentValue == TimeSpan.Zero)
                    orderTimeCurrentValue.CurrentValue = DateTime.Now.TimeOfDay;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}