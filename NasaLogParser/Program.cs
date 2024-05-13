using System.Diagnostics;
using NasaLogParser.Contexts;
using NasaLogParser.DataProcessors;
using NasaLogParser.Parsers;
using NasaLogParser.Providers;
using NasaLogParser.Services;

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

        await using var dbContext = new NasaLogDbContext();

        var saveLogRecordsService = new SaveLogRecords(dbContext);
        
        // Ensure the database is created and apply any pending migrations
        await dbContext.Database.EnsureCreatedAsync(source.Token);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        var logRecord = await provider.GetLogRecordAsync(source.Token);
        
        while (logRecord is not null)
        {
            try
            {
                var logRecordData = await processor.ProcessAsync(parser.Parse(logRecord), source.Token);

                await saveLogRecordsService.SaveAsync(logRecordData, source.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            logRecord = await provider.GetLogRecordAsync(source.Token);
        }
        
        stopwatch.Stop();

        Console.WriteLine($"Time parsing: {stopwatch.ElapsedMilliseconds}");
    }
}