namespace NasaLogParser.Entities.Data;

public class LogRecord
{
    public int Id { get; set; }
    
    public string Ip { get; set; }
    
    public DateTime Date { get; set; }
    
    public string Method { get; set; }
    
    public string Path { get; set; }
    
    public string Protocol { get; set; }
    
    public int StatusCode { get; set; }
    
    public int Size { get; set; }
    
    public string CountryCode { get; set; }

    public LogRecord(
        string ip, 
        DateTime date, 
        string method, 
        string path, 
        string protocol, 
        int statusCode, 
        int size, 
        string countryCode)
    {
        Ip = ip;
        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        Method = method;
        Path = path;
        Protocol = protocol;
        StatusCode = statusCode;
        Size = size;
        CountryCode = countryCode;
    }
}