namespace Asteroids.Integrations.Nasa
{
    public class NasaApiException : ApplicationException
    {
        public NasaApiException(string message) : base(message)
        {
        }
    }
}
