using NasaLogParser.DataProcessors;
using NasaLogParser.Parsers;
using NasaLogParser.Providers;

namespace NasaLogParser;

class Program
{
    static async Task Main(string[] args)
    {
        CancellationTokenSource source = new CancellationTokenSource();
        
        string path = @"C:\Users\Alestin\Downloads\NASA_access_log_Jul95/access_log_Jul95";

        await using var stream = new FileStream(path, FileMode.Open);
        using var reader = new StreamReader(stream);

        ILogProvider provider = new LogProvider(reader);
        ILogRecordParser parser = new LogRecordParser();
        ILogProcessor processor = new LogProcessor();

        var logRecord = await provider.GetLogRecordAsync(source.Token);
        //logRecord = await provider.GetLogRecordAsync(source.Token);

        if (logRecord is not null)
        {
            try
            {
                var logRecordDto = parser.Parse(logRecord);
                var logRecordData = await processor.ProcessAsync(logRecordDto, source.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}