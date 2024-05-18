using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder
            .Property(p => p.OrderId)
            .IsRequired();

        builder
            .Property(p => p.OrderDate);

        builder
            .Property(p => p.OrderTime);
    }
}