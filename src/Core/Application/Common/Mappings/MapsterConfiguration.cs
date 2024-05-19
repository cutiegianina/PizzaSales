using Application.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mappings;
public static class MapsterConfiguration
{
    public static void ConfigureMappings()
    {
        TypeAdapterConfig<PizzaDto, Pizza>.NewConfig();
        TypeAdapterConfig<Pizza, PizzaDto>.NewConfig();
        TypeAdapterConfig<OrderDto, Customer>.NewConfig();
        TypeAdapterConfig<Customer, OrderDto>.NewConfig();
        TypeAdapterConfig<OrderDetailDto, OrderDetail>.NewConfig();
        TypeAdapterConfig<OrderDetail, OrderDetailDto>.NewConfig();
        TypeAdapterConfig<PizzaTypeDto, PizzaType>.NewConfig();
        TypeAdapterConfig<PizzaType, PizzaTypeDto>.NewConfig();
        TypeAdapterConfig<Customer, CustomerDto>.NewConfig();
        TypeAdapterConfig<CustomerDto, Customer>.NewConfig();
    }
}