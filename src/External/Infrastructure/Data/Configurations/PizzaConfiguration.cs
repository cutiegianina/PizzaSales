using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.ToTable("Pizzas");

        builder.HasKey(p => p.PizzaId);

        builder
            .Property(p => p.PizzaId)
            .IsRequired();

        builder
            .HasOne(p => p.PizzaType)
            .WithMany()
            .HasForeignKey(p => p.PizzaTypeId);

        builder
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
    }
}