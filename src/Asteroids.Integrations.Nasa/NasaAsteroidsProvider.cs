using System.Text.Json;
using Asteroids.Contract;

namespace Asteroids.Integrations.Nasa;

public class NasaAsteroidsProvider : IAsteroidsProvider
{
    private const string ASTEROIDS_API_PATH = "neo/rest/v1/feed";
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public NasaAsteroidsProvider(HttpClient httpClient, NasaApiConfig config)
    {
        _httpClient = httpClient;
        _apiKey = config.ApiKey;
    }

    /// <summary>
    /// Loads near earth Asteroid information from NASA API
    /// </summary>
    /// <param name="days"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public async Task<IEnumerable<IAsteroid>> GetAsteroidsByDaysAsync(int days)
    {
        if (days is < 1 or > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(days), $"{nameof(days)} must be between 1 and 7.");
        }

        try
        {
            var nasaAsteroidsResponse = await GetAsteroidDataFromNasaApi(days);
            var mapper = new NasaAsteroidsMapper();

            return nasaAsteroidsResponse
                .NearEarthObjects
                .Values
                .SelectMany(x => x)
                .Select(x => mapper.MapTo(x))
                .ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<NasaAsteroidsResponse> GetAsteroidDataFromNasaApi(int days)
    {
        var uri = BuildRequestUri(days);
        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            throw new NasaApiException($"{response.Content.ReadAsStringAsync()}");
        }

        var stream = await response.Content.ReadAsStreamAsync();
        var data = await JsonSerializer.DeserializeAsync<NasaAsteroidsResponse>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return data;
    }

    private string BuildRequestUri(int days)
    {
        const string FORMAT = "yyy-MM-dd";
        string startDate = DateTime.UtcNow.ToString(FORMAT);
        string endDate = DateTime.UtcNow.AddDays(days).ToString(FORMAT);
        return $"{ASTEROIDS_API_PATH}?start_date={startDate}&end_date={endDate}&api_key={_apiKey}";
    }
}