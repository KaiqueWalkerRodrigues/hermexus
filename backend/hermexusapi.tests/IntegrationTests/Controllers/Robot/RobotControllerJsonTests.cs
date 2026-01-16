using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;

namespace hermexusapi.tests.IntegrationTests.Controllers.Robot
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class RobotControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static RobotDTO? _robot;

        public RobotControllerJsonTests(MySQLFixture sqlFixture)
        {
            _fixture = sqlFixture;

            var factory = new CustomWebApplicationFactory<Program>(_fixture.ConnectionString);
            _httpClient = factory.CreateClient();

            if (_fixture.UserToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _fixture.UserToken.Access_token);
            }
        }

        [Fact(DisplayName = "01 - Create Robot")]
        [TestPriority(1)]
        public async Task CreateRobot()
        {
            // Arrange
            var newRobot = new RobotDTO { Is_active = true, Name = "IA Test" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/robot/v1", newRobot);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<RobotDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Is_active.Should().BeTrue();
            created.Name.Should().Be("IA Test");
            _robot = created;
        }

        [Fact(DisplayName = "02 - Update Robot")]
        [TestPriority(2)]
        public async Task Test_UpdateRobot()
        {
            // Arrange
            _robot?.Name = "IA Test updated";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/robot/v1", _robot);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<RobotDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_robot?.Id);
            updated.Is_active.Should().BeTrue();
            updated.Name.Should().Be(_robot?.Name);
            _robot = updated;
        }

        [Fact(DisplayName = "03 - Find Robot by ID")]
        [TestPriority(3)]
        public async Task Test_FindRobotById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/robot/v1/{_robot?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundRole = await response.Content
                .ReadFromJsonAsync<RobotDTO>();
            foundRole.Should().NotBeNull();
            foundRole.Id.Should().Be(_robot?.Id);
            foundRole.Is_active.Should().Be(_robot?.Is_active);
            foundRole.Name.Should().Be(_robot?.Name);
        }

        [Fact(DisplayName = "04 - Find All Robots")]
        [TestPriority(4)]
        public async Task Test_FindAllRobots()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/robot/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<RobotDTO>>();
            page.Should().NotBeNull();
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var robots = page?.List;

            robots.Should().NotBeNull();
            robots.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete Robot")]
        [TestPriority(5)]
        public async Task Test_DeleteRobot()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/robot/v1/{_robot?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
