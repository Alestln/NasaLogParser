using NasaLogParser.Entities.Data;
using NasaLogParser.Entities.DTOs;

namespace NasaLogParser.DataProcessors;

public interface ILogProcessor
{
    Task<LogRecord> ProcessAsync(LogRecordDto logRecordDto, CancellationToken cancellationToken = default);
}