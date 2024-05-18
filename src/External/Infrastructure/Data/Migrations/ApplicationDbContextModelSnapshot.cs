﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("OrderTime")
                        .HasColumnType("time");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PizzaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PizzaId");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Pizza", b =>
                {
                    b.Property<string>("PizzaId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PizzaTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PizzaId");

                    b.HasIndex("PizzaTypeId");

                    b.ToTable("Pizzas", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PizzaType", b =>
                {
                    b.Property<string>("PizzaTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PizzaTypeId");

                    b.ToTable("PizzaTypes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Pizza", "Pizza")
                        .WithMany()
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Domain.Entities.Pizza", b =>
                {
                    b.HasOne("Domain.Entities.PizzaType", "PizzaType")
                        .WithMany()
                        .HasForeignKey("PizzaTypeId");

                    b.Navigation("PizzaType");
                });
#pragma warning restore 612, 618
        }
    }
}