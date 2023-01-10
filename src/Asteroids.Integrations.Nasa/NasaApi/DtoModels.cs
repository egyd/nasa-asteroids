using System.Text.Json.Serialization;

namespace Asteroids.Integrations.Nasa;

internal class NasaAsteroidsResponse
{
    [JsonPropertyName("links")]
    public WelcomeLinks Links { get; set; }

    [JsonPropertyName("element_count")]
    public int ElementsCount { get; set; }

    [JsonPropertyName("near_earth_objects")]
    public Dictionary<DateTime, List<NearEarthObject>> NearEarthObjects { get; set; }
}

internal class WelcomeLinks
{
    [JsonPropertyName("next")]
    public Uri Next { get; set; }

    [JsonPropertyName("previous")]
    public Uri Previous { get; set; }

    [JsonPropertyName("self")]
    public Uri Self { get; set; }
}

internal class NearEarthObject
{
    [JsonPropertyName("links")]
    public Links Links { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("neo_reference_id")]
    public string NeoReferenceId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nasa_jpl_url")]
    public string NasaJplUrl { get; set; }

    [JsonPropertyName("absolute_magnitude_h")]
    public double AbsoluteMagnitudeH { get; set; }

    [JsonPropertyName("estimated_diameter")]
    public EstimatedDiameterData EstimatedDiameterData { get; set; }

    [JsonPropertyName("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; set; }

    [JsonPropertyName("close_approach_data")]
    public CloseApproachData[] CloseApproachData { get; set; }

    [JsonPropertyName("is_sentry_object")]
    public bool IsSentryObject { get; set; }
}

internal class EstimatedDiameterData
{
    [JsonPropertyName("kilometers")]
    public EstimatedDiametr Kilometers { get; set; }

    [JsonPropertyName("meters")]
    public EstimatedDiametr Meters { get; set; }

    [JsonPropertyName("miles")]
    public EstimatedDiametr Miles { get; set; }

    [JsonPropertyName("feet")]
    public EstimatedDiametr Feet { get; set; }
}

internal class EstimatedDiametr
{
    [JsonPropertyName("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonPropertyName("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

internal class CloseApproachData
{
    [JsonPropertyName("close_approach_date")]
    public string CloseApproachDate { get; set; }

    [JsonPropertyName("close_approach_date_full")]
    public string CloseApproachDateFull { get; set; }

    [JsonPropertyName("epoch_date_close_approach")]
    public long EpochDateCloseApproach { get; set; }

    [JsonPropertyName("relative_velocity")]
    public RelativeVelocity RelativeVelocity { get; set; }

    [JsonPropertyName("miss_distance")]
    public MissDistance MissDistance { get; set; }

    [JsonPropertyName("orbiting_body")]
    public string OrbitingBody { get; set; }
}

internal class RelativeVelocity
{
    [JsonPropertyName("kilometers_per_second")]
    public string KilometersPerSecond { get; set; }

    [JsonPropertyName("kilometers_per_hour")]
    public string KilometersPerHour { get; set; }

    [JsonPropertyName("miles_per_hour")]
    public string MilesPerHour { get; set; }
}

internal class MissDistance
{
    [JsonPropertyName("astronomical")]
    public string Astronomical { get; set; }

    [JsonPropertyName("lunar")]
    public string Lunar { get; set; }

    [JsonPropertyName("kilometers")]
    public string Kilometers { get; set; }

    [JsonPropertyName("miles")]
    public string Miles { get; set; }
}

internal class Links
{
    [JsonPropertyName("self")]
    public string Self { get; set; }
}