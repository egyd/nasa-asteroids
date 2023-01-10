namespace Asteroids.Contract;

public interface IAsteroidsProvider
{
    Task<IEnumerable<IAsteroid>> GetAsteroidsByDaysAsync(int days);
}