using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace hermexusapi.tests.IntegrationTests.Controllers.Permission
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class PermissionControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static PermissionDTO? _permission;

        public PermissionControllerJsonTests(MySQLFixture sqlFixture)
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

        [Fact(DisplayName = "01 - Create Permission")]
        [TestPriority(1)]
        public async Task CreatePermission()
        {
            // Arrange
            var newPermission = new PermissionDTO { 
                Name = "Create Users",
                Description = "permission to create a user on this system"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/permission/v1", newPermission);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<PermissionDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Name.Should().Be("Create Users");
            created.Description.Should().Be("permission to create a user on this system");
            _permission = created;
        }

        [Fact(DisplayName = "02 - Update Permission")]
        [TestPriority(2)]
        public async Task Test_UpdatePermission()
        {
            // Arrange
            _permission?.Name = "Create Users updated";
            _permission?.Description = "permission to create a user on this system updated";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/permission/v1", _permission);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<PermissionDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_permission?.Id);
            updated.Name.Should().Be(_permission?.Name);
            updated.Description.Should().Be(_permission?.Description);
            _permission = updated;
        }

        [Fact(DisplayName = "03 - Find Permission by ID")]
        [TestPriority(3)]
        public async Task Test_FindPermissionById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/permission/v1/{_permission?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundPermission = await response.Content
                .ReadFromJsonAsync<PermissionDTO>();
            foundPermission.Should().NotBeNull();
            foundPermission.Id.Should().Be(_permission?.Id);
            foundPermission.Name.Should().Be(_permission?.Name);
            foundPermission.Description.Should().Be(_permission?.Description);
        }

        [Fact(DisplayName = "04 - Find All Permissions")]
        [TestPriority(4)]
        public async Task Test_FindAllPermissions()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/permission/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<PermissionDTO>>();
            page.Should().NotBeNull();
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var permissions = page?.List;

            permissions.Should().NotBeNull();
            permissions.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete Permission")]
        [TestPriority(5)]
        public async Task Test_DeletePermission()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/permission/v1/{_permission?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
