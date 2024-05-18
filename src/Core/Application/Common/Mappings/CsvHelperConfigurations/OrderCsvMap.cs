using Application.Dtos;
using CsvHelper.Configuration;

namespace Application.Common.Mappings.CsvHelperConfigurations;
public class OrderCsvMap : ClassMap<OrderDto>
{
    public OrderCsvMap()
    {
        Map(p => p.OrderId).Name("order_id");
        Map(p => p.OrderDate).Name("date").Default(DateTime.MinValue);
        Map(p => p.OrderTime).Name("time").Default(TimeSpan.Zero);
    }
}