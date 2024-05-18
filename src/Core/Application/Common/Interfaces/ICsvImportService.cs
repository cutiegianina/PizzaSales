namespace Application.Common.Interfaces;
public interface ICsvImportService<T, TMap>
{
    Task<List<T>> ImportCsvAsync(Stream fileStream);
}