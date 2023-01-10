namespace Asteroids.Contract;

public interface IAsteroid
{
    string Id { get; }

    string Name { get; }

    bool IsPotentiallyHazardous { get; }

    double AverageDiameter { get; }

    string Velocity { get; }

    DateTime Date { get; }
}