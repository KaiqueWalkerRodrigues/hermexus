using Docker.DotNet.Models;
using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace hermexusapi.tests.IntegrationTests.Controllers.Roles
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class RoleControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static RoleDTO? _role;

        public RoleControllerJsonTests(MySQLFixture sqlFixture)
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

        [Fact(DisplayName = "01 - Create Role")]
        [TestPriority(1)]
        public async Task CreateRole_ShouldReturnSuccess()
        {
            // Arrange
            var newRole = new RoleDTO { Name = "administrator", Description = "administrator of system" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/role/v1", newRole);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<RoleDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Name.Should().Be("administrator");
            created.Description.Should().Be("administrator of system");
            _role = created;
        }

        [Fact(DisplayName = "02 - Update Role")]
        [TestPriority(2)]
        public async Task Test_UpdateRole()
        {
            // Arrange
            _role?.Name = "administrator updated";
            _role?.Description = "administrator of this system";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/role/v1", _role);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<RoleDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_role?.Id);
            updated.Name.Should().Be(_role?.Name);
            updated.Description.Should().Be(_role?.Description);
            _role = updated;
        }

        [Fact(DisplayName = "03 - Find Role by ID")]
        [TestPriority(3)]
        public async Task Test_FindRoleById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/role/v1/{_role?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundRole = await response.Content
                .ReadFromJsonAsync<RoleDTO>();
            foundRole.Should().NotBeNull();
            foundRole.Id.Should().Be(_role?.Id);
            foundRole.Name.Should().Be(_role?.Name);
            foundRole.Description.Should().Be(_role?.Description);
        }

        [Fact(DisplayName = "04 - Find All Roles")]
        [TestPriority(4)]
        public async Task Test_FindAllRoles()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/role/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<RoleDTO>>();
            page.Should().NotBeNull();
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var roles = page?.List;

            roles.Should().NotBeNull();
            roles.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete Role")]
        [TestPriority(5)]
        public async Task Test_DeleteRole()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/user/v1/{_role?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
