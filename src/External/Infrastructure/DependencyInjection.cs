using Application.Common.Interfaces.Data;
using Infrastructure.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Interceptors;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration config)
    {
        string? connectionString = config.GetConnectionString("ApplicationDbContext");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, optionsBuilder) =>
        {
            var auditableInterceptor = sp.GetRequiredService<OrderDateTimeInterceptor>();
            optionsBuilder.UseSqlServer(connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                .AddInterceptors(auditableInterceptor);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<OrderDateTimeInterceptor>();
        return services;
    }
}