namespace NasaLogParser.Providers;

public class LogProvider(StreamReader streamReader) : ILogProvider
{
    public async Task<string?> GetLogRecordAsync(CancellationToken cancellationToken = default)
    {
        return await streamReader.ReadLineAsync(cancellationToken);
    }
}