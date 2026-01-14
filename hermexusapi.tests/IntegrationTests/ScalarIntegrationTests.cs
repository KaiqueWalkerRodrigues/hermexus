using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using hermexusapi.tests.IntegrationTests.Tools;

namespace hermexusapi.Tests.IntegrationTests
{
    public class ScalarIntegrationTests
        : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        public ScalarIntegrationTests(MySQLFixture mysqlFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(
                mysqlFixture.ConnectionString);
            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost:8080")
                }
            );
        }

        [Fact]
        public async Task ScalarUI_ShouldReturnScalarUI()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/scalar/");
            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
            content.Should().Contain("<script src=\"scalar.js\"");
        }
    }
}