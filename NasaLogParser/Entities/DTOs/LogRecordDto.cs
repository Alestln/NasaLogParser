namespace NasaLogParser.Entities.DTOs;

public record LogRecordDto(
    string Ip,
    DateTime Date,
    string Method,
    string Path,
    string Protocol,
    int StatusCode,
    int Size);