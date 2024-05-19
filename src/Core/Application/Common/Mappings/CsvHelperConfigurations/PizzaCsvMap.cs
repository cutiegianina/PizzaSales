using Application.Dtos;
using CsvHelper.Configuration;

namespace Application.Common.Mappings.CsvHelperConfigurations;

public class PizzaCsvMap : ClassMap<PizzaDto>
{
    public PizzaCsvMap()
    {
        Map(p => p.PizzaId).Name("pizza_id");
        Map(p => p.PizzaTypeId).Name("pizza_type_id");
        Map(p => p.Size).Name("size");
        Map(p => p.Price).Name("price");
    }
}