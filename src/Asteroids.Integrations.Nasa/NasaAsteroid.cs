using Asteroids.Contract;

namespace Asteroids.Integrations.Nasa;

internal class NasaAsteroid : IAsteroid
{
    public string Id { get; init; }

    public string Name { get; init; }

    public bool IsPotentiallyHazardous { get; init; }

    public double AverageDiameter { get; init; }

    public string Velocity { get; init; }

    public DateTime Date { get; init; }
}