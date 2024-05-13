using NasaLogParser.Contexts;
using NasaLogParser.Entities.Data;

namespace NasaLogParser.Services;

public class SaveLogRecords (NasaLogDbContext context)
{
    public async Task SaveAsync(LogRecord logRecords, CancellationToken cancellationToken)
    {
        await context.AddAsync(logRecords, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}