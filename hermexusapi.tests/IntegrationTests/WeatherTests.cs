using hermexusapi.tests.IntegrationTests.Tools;
using System.Net.Http.Json;

namespace hermexusapi.tests.UniTests
{
    public class WeatherTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public WeatherTests(MySQLFixture fixture)
        {
            _factory = new CustomWebApplicationFactory<Program>(fixture.ConnectionString);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Weather_ResponseTest_ShouldReturnExpectedJsonStructure()
        {
            // Arrange
            var route = "weatherforecast";

            // Act
            var response = await _client.GetAsync(route);

            // Assert
            response.EnsureSuccessStatusCode();

            var forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastResponse>>();

            Assert.NotNull(forecasts);
            Assert.Equal(5, forecasts.Count);

            Assert.All(forecasts, item =>
            {
                Assert.Equal("Freezing", item.Summary);
                Assert.True(item.Date >= DateOnly.FromDateTime(DateTime.Now));
            });
        }
    }

    public class WeatherForecastResponse
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string? Summary { get; set; }
    }
}