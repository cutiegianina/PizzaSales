using Application.Dtos;
using CsvHelper.Configuration;

namespace Application.Common.Mappings.CsvHelperConfigurations;

public class PizzaTypeCsvMap : ClassMap<PizzaTypeDto>
{
    PizzaTypeCsvMap()
    {
        Map(p => p.PizzaTypeId).Name("pizza_type_id");
        Map(p => p.Name).Name("name");
        Map(p => p.Category).Name("category");
        Map(p => p.Ingredients).Name("ingredients");
    }
}