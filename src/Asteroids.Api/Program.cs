using System.ComponentModel.DataAnnotations;
using Asteroids.Contract;
using Asteroids.Integrations.Nasa;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var nasaApiConfig = new NasaApiConfig();
builder.Configuration.GetSection(NasaApiConfig.Key).Bind(nasaApiConfig);
ValidateNasaApiConfiguration(nasaApiConfig);

builder.Services.AddSingleton(nasaApiConfig);

builder.Services.AddHttpClient<IAsteroidsProvider, NasaAsteroidsProvider>(client =>
{
    client.BaseAddress = new Uri(nasaApiConfig.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(nasaApiConfig.TimeoutSeconds);
});

builder.Services.AddScoped<IAsteroidsService, NasaAsteroidsService>();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp => exceptionHandlerApp.Run(
    async context => await Results.Problem().ExecuteAsync(context)));

app.MapGet("/asteroids", async (
        [FromQuery][Required][Range(1,7)] int? days, 
        IAsteroidsService asteroidsService)
    =>
{
    if (days is null)
    {
        return Results.BadRequest($"'{nameof(days)}' query parameter is required.");
    }
    if (days is < 1 or > 7)
    {
        return Results.BadRequest($"'{nameof(days)}' query parameter must be between 1 and 7.");
    }
    
    var asteroids = await asteroidsService.GetMostRiskiestAsync(days.Value);

    return Results.Ok(asteroids.Select(x => new
    {
        Name = x.Name,
        Diameter = x.AverageDiameter,
        Velocity = x.Velocity,
        Date = x.Date.Date
    }));
});

app.Run();


void ValidateNasaApiConfiguration(NasaApiConfig config)
{
    if (nasaApiConfig == null
        || string.IsNullOrWhiteSpace(config.BaseUrl)
        || string.IsNullOrWhiteSpace(config.ApiKey))
    {
        throw new ApplicationException("Missing NASA API configuration.");
    }
}
