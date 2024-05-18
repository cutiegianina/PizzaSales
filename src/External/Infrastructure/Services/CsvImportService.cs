using Application.Common.Interfaces;
using Application.Common.Mappings.CsvHelperConfigurations;
using Application.Dtos;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Infrastructure.Services;
public class CsvImportService<T, TMap> : ICsvImportService<T, TMap>
    where T     : class
    where TMap  : ClassMap<T>
{
    public async Task<List<T>> ImportCsvAsync(Stream fileStream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<TMap>();
        var records = csv.GetRecords<T>().ToList();

        // Process records here, for example:
        foreach (var record in records)
        {
            // Map DTO to domain entity and save to database
            // Example:
            // var entity = new YourEntity { Property1 = record.Field1, Property2 = record.Field2, ... };
            // _context.YourEntities.Add(entity);
        }

        // Save changes to database, if applicable
        // await _context.SaveChangesAsync();
        return await Task.FromResult(records);
    }
}
