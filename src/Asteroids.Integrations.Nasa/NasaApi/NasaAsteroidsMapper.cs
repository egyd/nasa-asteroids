using Asteroids.Contract;

namespace Asteroids.Integrations.Nasa;

internal class NasaAsteroidsMapper
{
    internal IAsteroid MapTo(NearEarthObject nearEarthObject)
    {
        var closeApproachData = nearEarthObject.CloseApproachData.FirstOrDefault(); //TODO null check?
        var estimatedDiameterData = nearEarthObject.EstimatedDiameterData.Kilometers;

        return new NasaAsteroid
        {
            Id = nearEarthObject.Id,
            Name = nearEarthObject.Name,
            IsPotentiallyHazardous = nearEarthObject.IsPotentiallyHazardousAsteroid,
            Date = DateTime.Parse(closeApproachData.CloseApproachDate),
            Velocity = closeApproachData.RelativeVelocity.KilometersPerHour,
            AverageDiameter = CalculateDiameter(
                estimatedDiameterData.EstimatedDiameterMin,
                estimatedDiameterData.EstimatedDiameterMax)
        };
    }

    private static double CalculateDiameter(double min, double max)
    {
        return CalculateCubeRoot((max + min) * (max * max + min * min)) / 4;
    }

    private static double CalculateCubeRoot(double val)
    {
        return Math.Pow(val, 1.0 / 3.0);
    }
}