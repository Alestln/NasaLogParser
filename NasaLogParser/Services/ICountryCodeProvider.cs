namespace NasaLogParser.Services;

public interface ICountryCodeProvider
{
    Task<string> GetCountryCode(string input, CancellationToken cancellationToken = default);
}