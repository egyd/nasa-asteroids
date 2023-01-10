using Asteroids.Contract;

namespace Asteroids.Integrations.Nasa;

public class NasaAsteroidsService : IAsteroidsService
{
    private IAsteroidsProvider AsteroidsProvider { get; }

    public NasaAsteroidsService(IAsteroidsProvider asteroidsProvider)
    {
        AsteroidsProvider = asteroidsProvider;
    }

    public async Task<IEnumerable<IAsteroid>> GetMostRiskiestAsync(int days, int count = 3)
    {
        var asteroids = await AsteroidsProvider.GetAsteroidsByDaysAsync(days);
        
        return asteroids
            .Where(x => x.IsPotentiallyHazardous)
            .OrderByDescending(x => x.AverageDiameter)
            .Take(count);
    }
}