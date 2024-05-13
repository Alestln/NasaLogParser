using System.Net;
using NasaLogParser.Entities.Data;
using NasaLogParser.Entities.DTOs;
using NasaLogParser.Services;

namespace NasaLogParser.DataProcessors;

public class LogProcessor : ILogProcessor
{
    public async Task<LogRecord> ProcessAsync(LogRecordDto logRecordDto, CancellationToken cancellationToken)
    {
        return new LogRecord(
            logRecordDto.Ip,
            logRecordDto.Date,
            logRecordDto.Method,
            logRecordDto.Path,
            logRecordDto.Protocol,
            logRecordDto.StatusCode,
            logRecordDto.Size,
            await GetCountryCode(logRecordDto.Ip, cancellationToken));
    }

    private async Task<string> GetCountryCode(string logRecordIp, CancellationToken cancellationToken)
    {
        if (IPAddress.TryParse(logRecordIp, out var ip))
        {
            try
            {
                return await new IpCountryCodeProvider().GetCountryCode(ip.ToString(), cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return await new DomainCountryCodeProvider().GetCountryCode(logRecordIp, cancellationToken);
    }
}