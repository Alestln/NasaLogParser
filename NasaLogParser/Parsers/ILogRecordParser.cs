using NasaLogParser.Entities.DTOs;

namespace NasaLogParser.Parsers;

public interface ILogRecordParser
{
    LogRecordDto Parse(ReadOnlySpan<char> logRecord);
}