namespace Application.Common.Interfaces;
/// <summary>
/// Interface for a service that imports data from a CSV file and converts it into a list of objects.
/// </summary>
/// <typeparam name="TResult">The type of the objects to which the CSV data is converted.</typeparam>
/// <typeparam name="TMapper">The type of the mapper used for converting CSV data to objects.</typeparam>
public interface ICsvImportService<TResult, TMapper>
{
    /// <summary>
    /// Converts a CSV file into a C# list of objects.
    /// </summary>
    /// <param name="fileStream"></param>
    /// <returns>Returns a generic type of list from a CSV file</returns>
    Task<List<TResult>> ImportCsvAsync(Stream fileStream);
}