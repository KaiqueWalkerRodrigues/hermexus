using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace hermexusapi.tests.IntegrationTests.Controllers.Users
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class UserControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private static UserDTO? _user;
        private static TokenDTO? _token;
        public UserControllerJsonTests(MySQLFixture sqlFixture)
        {
            var factory = new CustomWebApplicationFactory<Program>(
                sqlFixture.ConnectionString);
            _httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("http://localhost:8080")
                }
            );
        }

        [Fact(DisplayName = "00 - Create User")]
        [TestPriority(0)]
        public async Task Test_CreateUser()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token?.AccessToken);

            var request = new UserDTO
            {
                IsActive = true,
                Username = "joao.macedo",
                Password = "joao123",
                Name = "João Macedo"
            };
            // Act
            var response = await _httpClient
                .PostAsJsonAsync("/api/user/v1", request);

            // Assert
            response.EnsureSuccessStatusCode();

            var created = await response.Content
                .ReadFromJsonAsync<UserDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Username.Should().Be("joao.macedo");
            created.Password.Should().NotBe("joao123");
            created.Name.Should().Be("João Macedo");
            created.IsActive.Should().BeTrue();
            _user = created;
        }

        [Fact(DisplayName = "01 - Sign In")]
        [TestPriority(1)]
        public async Task Test_SignIn()
        {
            // Arrange
            var credentials = new UserDTO
            {
                Username = "joao.macedo",
                Password = "joao123"
            };
            // Act
            var response = await _httpClient.PostAsJsonAsync(
                "/api/auth/signin", credentials);
            // Assert
            response.EnsureSuccessStatusCode();

            var token = await response.Content
                .ReadFromJsonAsync<TokenDTO>();

            token.Should().NotBeNull();
            token.AccessToken.Should().NotBeNullOrWhiteSpace();
            token.RefreshToken.Should().NotBeNullOrWhiteSpace();
            _token = token;
        }

        [Fact(DisplayName = "02 - Update User")]
        [TestPriority(2)]
        public async Task Test_UpdateUser()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token?.AccessToken);

            _user?.Name = "João Macedo Pinate";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/user/v1", _user);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<UserDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().BeGreaterThan(0);
            updated.Name.Should().Be(_user?.Name);
            updated.Username.Should().Be(_user?.Username);
            updated.IsActive.Should().BeTrue();
            _user = updated;
        }

        [Fact(DisplayName = "03 - Find User by ID")]
        [TestPriority(3)]
        public async Task Test_FindUserById()
        {
            // Arrange & Act
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token?.AccessToken);

            var response = await _httpClient
                .GetAsync($"/api/user/v1/{_user?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundUser = await response.Content
                .ReadFromJsonAsync<UserDTO>();
            foundUser.Should().NotBeNull();
            foundUser.Id.Should().Be(_user?.Id);
            foundUser.Name.Should().Be(_user?.Name);
            foundUser.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = "04 - Find All Users")]
        [TestPriority(4)]
        public async Task Test_FindAllUsers()
        {
            // Arrange & Act
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token?.AccessToken);

            var response = await _httpClient
                .GetAsync("/api/user/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<UserDTO>>();
            page.Should().NotBeNull();
            page.CurrentPage.Should().Be(1);
            page.PageSize.Should().Be(10);

            var users = page?.List;

            users.Should().NotBeNull();
            users.Count.Should().BeGreaterThan(0);

            page!.CurrentPage.Should().BeGreaterThan(0);
            page.TotalResults.Should().BeGreaterThan(0);
            page.PageSize.Should().BeGreaterThan(0);
            page.SortDirections.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete User")]
        [TestPriority(5)]
        public async Task Test_DeleteUser()
        {
            // Arrange & Act
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _token?.AccessToken);

            var response = await _httpClient
                .DeleteAsync($"/api/user/v1/{_user?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}