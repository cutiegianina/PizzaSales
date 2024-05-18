using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("OrderDetails");

        builder
            .Property(p => p.OrderDetailId)
            .IsRequired();

        builder
            .HasOne(p => p.Order)
            .WithMany()
            .HasForeignKey(p => p.OrderId);

        builder
            .Property(p => p.OrderId);

        builder
            .Property(p => p.PizzaId);

        builder
            .HasOne(p => p.Pizza)
            .WithMany()
            .HasForeignKey(p => p.PizzaId);
    }
}