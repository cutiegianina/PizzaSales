using Application.Dtos;
using CsvHelper.Configuration;

namespace Application.Common.Mappings.CsvHelperConfigurations;
public class CustomerCsvMap : ClassMap<CustomerDto>
{
    public CustomerCsvMap()
    {
        Map(p => p.CustomerId).Name("customer_id");
        Map(p => p.Name).Name("name");
        Map(p => p.Email).Name("email");
        Map(p => p.PhoneNumber).Name("phone_number");
        Map(p => p.Address).Name("address");
    }
}