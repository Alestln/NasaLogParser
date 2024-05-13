namespace NasaLogParser.Providers;

public interface ILogProvider
{
    Task<string?> GetLogRecordAsync(CancellationToken cancellationToken = default);
}