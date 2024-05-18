using Application.Common.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace Infrastructure.Services;
public class CsvImportService<TResult, TMapper> : ICsvImportService<TResult, TMapper>
    where TResult     : class
    where TMapper : ClassMap<TResult>
{
    public async Task<List<TResult>> ImportCsvAsync(Stream fileStream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, config);

        csv.Context.RegisterClassMap<TMapper>();
        var records = csv.GetRecords<TResult>().ToList();

        return await Task.FromResult(records);
    }
}