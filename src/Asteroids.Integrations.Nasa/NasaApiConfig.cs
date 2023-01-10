namespace Asteroids.Integrations.Nasa;

public class NasaApiConfig
{
    public const string Key = "NasaApi"; 

    public string BaseUrl { get; init; }

    public string ApiKey { get; init; }
    
    public int TimeoutSeconds { get; init; }
}