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
        TypeAdapterConfig<OrderDto, Order>.NewConfig();
        TypeAdapterConfig<Order, OrderDto>.NewConfig();
        TypeAdapterConfig<OrderDetailDto, OrderDetail>.NewConfig();
        TypeAdapterConfig<OrderDetail, OrderDetailDto>.NewConfig();
        TypeAdapterConfig<PizzaTypeDto, PizzaType>.NewConfig();
        TypeAdapterConfig<PizzaType, PizzaTypeDto>.NewConfig();
    }
}