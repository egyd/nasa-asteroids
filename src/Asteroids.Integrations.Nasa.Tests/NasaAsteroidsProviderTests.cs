using System.Net;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using RichardSzalay.MockHttp;
using FluentAssertions.Execution;

namespace Asteroids.Integrations.Nasa.Tests;

public class NasaAsteroidsProviderTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(8)]
    [InlineData(100)]
    public async Task Call_GetAsteroidsByDaysAsync_WithLessThanValidDays_ExceptionThrown(int days)
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        var httpClient = fixture.Create<HttpClient>();
        var nasaApiConfig = fixture.Create<NasaApiConfig>();
        var sut = new NasaAsteroidsProvider(httpClient, nasaApiConfig);

        //Act
        var action = () => sut.GetAsteroidsByDaysAsync(days);
       
        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();
    }

    [Fact]
    public async Task Call_GetAsteroidsByDaysAsync_NasaApiRespondsWithError_ExceptionThrown()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        const int days = 3;
        var nasaApiConfig = fixture.Create<NasaApiConfig>();

        var httpMessageHandlerMock = new MockHttpMessageHandler();
        httpMessageHandlerMock
            .When(HttpMethod.Get, "*")
            .Respond(HttpStatusCode.BadRequest);
        var httpClient = httpMessageHandlerMock.ToHttpClient();
        httpClient.BaseAddress = new Uri("http://localhost");

        var sut = new NasaAsteroidsProvider(httpClient, nasaApiConfig);

        //Act
        var action = () => sut.GetAsteroidsByDaysAsync(days);
       
        //Assert
        await action.Should().ThrowAsync<NasaApiException>();
    }

    [Fact]
    public async Task Call_GetAsteroidsByDaysAsync_NasaApiRespondsOk_DataSetReturns()
    {
        //Arrange
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        const int days = 3;
        var nasaApiConfig = fixture.Create<NasaApiConfig>();
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NasaApiResponseMock.json");
        var nasaSuccessResponseMock = await File.ReadAllTextAsync(filePath);
        var httpMessageHandlerMock = new MockHttpMessageHandler();
        httpMessageHandlerMock
            .When(HttpMethod.Get, "*")
            .Respond("application/json", nasaSuccessResponseMock);
        var httpClient = httpMessageHandlerMock.ToHttpClient();
        httpClient.BaseAddress = new Uri("http://localhost");

        var sut = new NasaAsteroidsProvider(httpClient, nasaApiConfig);

        //Act
        var result = await sut.GetAsteroidsByDaysAsync(days);

        //Assert
        using var scope = new AssertionScope();
        result.Should().NotBeNullOrEmpty();
    }
}