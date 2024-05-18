using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;
public class PizzaTypeConfiguration : IEntityTypeConfiguration<PizzaType>
{
    public void Configure(EntityTypeBuilder<PizzaType> builder)
    {
        builder.ToTable("PizzaTypes");
        builder
            .Property(p => p.PizzaTypeId)
            .IsRequired();
    }
}