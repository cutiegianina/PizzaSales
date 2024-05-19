using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(p => p.OrderId);

        builder
            .Property(p => p.OrderId)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.OrderDate);

        builder
            .Property(p => p.OrderTime);

        builder
            .Property(p => p.TotalAmount)
            .HasColumnType("decimal(18, 2)");
    }
}