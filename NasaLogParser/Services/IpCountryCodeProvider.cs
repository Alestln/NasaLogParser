using System.Text.Json.Nodes;

namespace NasaLogParser.Services;

public class IpCountryCodeProvider : ICountryCodeProvider
{
    private readonly HttpClient _httpClient = new();

    public async Task<string> GetCountryCode(string ip, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://ip-api.com/json/{ip}", cancellationToken);
            response.EnsureSuccessStatusCode();
            
            //return ParseCountryCodeFromJson(await response.Content.ReadAsStringAsync(cancellationToken));
            return ParseCountryCodeFromJson("{\"name\":\"John\", \"age\":30, \"car\":null}");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP error occurred: {ex.Message}");
        }
        catch (NullReferenceException ex)
        {
            throw new Exception($"{ex.Message} Ip: {ip}");
        }
    }
    
    private string ParseCountryCodeFromJson(string json)
    {
        var jsonObject = JsonNode.Parse(json);

        if (jsonObject is not null)
        {
            var countryCode = jsonObject["countryCode"]!.ToString();

            return countryCode;
        }

        throw new NullReferenceException("Failed to get country code from json.");
    }
}