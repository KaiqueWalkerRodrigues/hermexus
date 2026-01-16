using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;

namespace hermexusapi.tests.IntegrationTests.Controllers.Containers
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class CompanyControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static CompanyDTO? _company;

        public CompanyControllerJsonTests(MySQLFixture sqlFixture)
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

        [Fact(DisplayName = "01 - Create Company")]
        [TestPriority(1)]
        public async Task CreateCompany()
        {
            // Arrange
            var newCompany = new CompanyDTO {
                Name = "Test Company",
                Is_active = true,
                Legal_name = "Test Company LTDA",
                Document_number = "55155"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/company/v1", newCompany);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<CompanyDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Is_active.Should().BeTrue();
            created.Name.Should().Be("Test Company");
            created.Legal_name.Should().Be("Test Company LTDA");
            created.Document_number.Should().Be("55155");
            _company = created;
        }

        [Fact(DisplayName = "02 - Update Company")]
        [TestPriority(2)]
        public async Task Test_UpdateCompany()
        {
            // Arrange
            _company?.Name = "Test Company Update";
            _company?.Legal_name = "Test Company LTDA Update";
            _company?.Document_number = "551551";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/company/v1", _company);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<CompanyDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_company?.Id);
            updated.Is_active.Should().BeTrue();
            updated.Name.Should().Be(_company?.Name);
            updated.Legal_name.Should().Be(_company?.Legal_name);
            updated.Document_number.Should().Be(_company?.Document_number);
            _company = updated;
        }

        [Fact(DisplayName = "03 - Find Company by ID")]
        [TestPriority(3)]
        public async Task Test_FindSectorById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/company/v1/{_company?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundCompany = await response.Content
                .ReadFromJsonAsync<CompanyDTO>();
            foundCompany.Should().NotBeNull();
            foundCompany.Id.Should().Be(_company?.Id);
            foundCompany.Is_active.Should().BeTrue();
            foundCompany.Name.Should().Be(_company?.Name);
            foundCompany.Legal_name.Should().Be(_company?.Legal_name);
            foundCompany.Document_number.Should().Be(_company?.Document_number);
        }

        [Fact(DisplayName = "04 - Find All Company")]
        [TestPriority(4)]
        public async Task Test_FindAllSectors()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/company/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<CompanyDTO>>();
            page.Should().NotBeNull();
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var companies = page?.List;

            companies.Should().NotBeNull();
            companies.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete Company")]
        [TestPriority(5)]
        public async Task Test_DeleteCompany()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/company/v1/{_company?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
