using Application.Dtos;
using CsvHelper.Configuration;

namespace Application.Common.Mappings.CsvHelperConfigurations;
public class OrderDetailCsvMap : ClassMap<OrderDetailDto>
{
    public OrderDetailCsvMap()
    {
        Map(p => p.OrderDetailId).Name("order_details_id");
        Map(p => p.OrderId).Name("order_id");
        Map(p => p.PizzaId).Name("pizza_id");
        Map(p => p.Quantity).Name("quantity");
    }
}