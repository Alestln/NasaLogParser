using NasaLogParser.Entities.DTOs;

namespace NasaLogParser.Parsers;

public class LogRecordParser : ILogRecordParser
{
    public LogRecordDto Parse(ReadOnlySpan<char> logRecord)
    {
        // Ищем позиции пробелов
        var spaceIndexes = new int[9];
        spaceIndexes[0] = logRecord.IndexOf(' ');
        for (var i = 1; i < spaceIndexes.Length; i++)
        {
            spaceIndexes[i] = logRecord[(spaceIndexes[i - 1] + 1)..].IndexOf(' ') + spaceIndexes[i - 1] + 1;
        }
        
        var ip = logRecord[..spaceIndexes[0]].ToString();
        DateTime date = DateTime.ParseExact(
            logRecord.Slice(spaceIndexes[2] + 2, spaceIndexes[4] - spaceIndexes[2] - 3).ToString(),
            "dd/MMM/yyyy:HH:mm:ss zzz",
            System.Globalization.CultureInfo.InvariantCulture);
        var method = logRecord.Slice(spaceIndexes[4] + 2, spaceIndexes[5] - spaceIndexes[4] - 2).ToString();
        var path = logRecord.Slice(spaceIndexes[5] + 1, spaceIndexes[6] - spaceIndexes[5] - 1).ToString();
        var protocol = logRecord.Slice(spaceIndexes[6] + 1, spaceIndexes[7] - spaceIndexes[6] - 2).ToString();
        var statusCode = int.Parse(logRecord.Slice(spaceIndexes[7], spaceIndexes[8] - spaceIndexes[7]));
        var size = int.Parse(logRecord[(logRecord.LastIndexOf(' ') + 1)..]);

        return new LogRecordDto(ip, date, method, path, protocol, statusCode, size);
    }
}