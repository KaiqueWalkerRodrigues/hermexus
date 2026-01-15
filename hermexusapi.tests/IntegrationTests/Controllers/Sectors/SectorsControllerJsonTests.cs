using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace hermexusapi.tests.IntegrationTests.Controllers.Sectors
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class SectorControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static SectorDTO? _sector;

        public SectorControllerJsonTests(MySQLFixture sqlFixture)
        {
            _fixture = sqlFixture;

            var factory = new CustomWebApplicationFactory<Program>(_fixture.ConnectionString);
            _httpClient = factory.CreateClient();

            if (_fixture.UserToken != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _fixture.UserToken.AccessToken);
            }
        }

        [Fact(DisplayName = "01 - Create Sector")]
        [TestPriority(1)]
        public async Task CreateSector_ShouldReturnSuccess()
        {
            // Arrange
            var newSector = new SectorDTO { Name = "callcenter", Description = "callcenter of system", CompanyId = 1 };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/sector/v1", newSector);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<SectorDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.IsActive.Should().BeTrue();
            created.CompanyId.Should().Be(1);
            created.Name.Should().Be("callcenter");
            created.Description.Should().Be("callcenter of system");
            _sector = created;
        }

        [Fact(DisplayName = "02 - Update Sector")]
        [TestPriority(2)]
        public async Task Test_UpdateSector()
        {
            // Arrange
            _sector?.Name = "callcenter updated";
            _sector?.Description = "callcenter of system updated";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/sector/v1", _sector);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<SectorDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_sector?.Id);
            updated.CompanyId.Should().Be(_sector?.CompanyId);
            updated.IsActive.Should().BeTrue();
            updated.Name.Should().Be(_sector?.Name);
            updated.Description.Should().Be(_sector?.Description);
            _sector = updated;
        }

        [Fact(DisplayName = "03 - Find Sector by ID")]
        [TestPriority(3)]
        public async Task Test_FindSectorById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/sector/v1/{_sector?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundSector = await response.Content
                .ReadFromJsonAsync<SectorDTO>();
            foundSector.Should().NotBeNull();
            foundSector.Id.Should().Be(_sector?.Id);
            foundSector.CompanyId.Should().Be(_sector?.CompanyId);
            foundSector.IsActive.Should().BeTrue();
            foundSector.Name.Should().Be(_sector?.Name);
            foundSector.Description.Should().Be(_sector?.Description);
        }

        [Fact(DisplayName = "04 - Find All Sectors")]
        [TestPriority(4)]
        public async Task Test_FindAllSectors()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/sector/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<SectorDTO>>();
            page.Should().NotBeNull();
            page.CurrentPage.Should().Be(1);
            page.PageSize.Should().Be(10);

            var sectors = page?.List;

            sectors.Should().NotBeNull();
            sectors.Count.Should().BeGreaterThan(0);

            page!.CurrentPage.Should().BeGreaterThan(0);
            page.TotalResults.Should().BeGreaterThan(0);
            page.PageSize.Should().BeGreaterThan(0);
            page.SortDirections.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete Sector")]
        [TestPriority(5)]
        public async Task Test_DeleteSector()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/sector/v1/{_sector?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
