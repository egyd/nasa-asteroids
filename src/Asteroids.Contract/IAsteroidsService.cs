namespace Asteroids.Contract;

public interface IAsteroidsService
{
    Task<IEnumerable<IAsteroid>> GetMostRiskiestAsync(int days, int count = 3);
}