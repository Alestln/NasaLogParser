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
        try
        {
            if (IPAddress.TryParse(logRecordIp, out var ip))
            {
                return await new IpCountryCodeProvider().GetCountryCode(ip.ToString(), cancellationToken);
            }
            
            var ipAddresses = await Dns.GetHostAddressesAsync(logRecordIp, cancellationToken);
            if (ipAddresses.Length > 0)
            {
                return await new IpCountryCodeProvider().GetCountryCode(ipAddresses[0].ToString(), cancellationToken);   
            }

            Console.WriteLine("No IP addresses found for the domain.");
            return string.Empty;
        }
        catch (Exception ex)
        {
            throw new Exception($"Exception message: {ex.Message}\nIp: {logRecordIp}");
        }
    }
}