namespace NasaLogParser.Entities.Data;

public record LogRecord(
    string Ip,
    DateTime Date,
    string Method,
    string Path,
    string Protocol,
    int StatusCode,
    int Size,
    string CountryCode);