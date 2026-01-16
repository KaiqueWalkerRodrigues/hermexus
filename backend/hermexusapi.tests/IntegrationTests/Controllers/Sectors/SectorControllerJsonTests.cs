using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System.Net;
using System.Net.Http.Json;

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
        private static CompanyDTO? _company;

        public SectorControllerJsonTests(MySQLFixture sqlFixture)
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

        [Fact(DisplayName = "00 - Create Requiriments")]
        [TestPriority(0)]
        public async Task CreateRequiriments()
        {
            // Arrange
            var newCompany = new CompanyDTO { Name = "Test Company", Is_active = true, Legal_name = "Test Company LTDA", Document_number = "55155" };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/company/v1", newCompany);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<CompanyDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            _company = created;
        }

        [Fact(DisplayName = "01 - Create Sector")]
        [TestPriority(1)]
        public async Task CreateSector()
        {
            // Arrange
            var newSector = new SectorDTO { Name = "callcenter", Description = "callcenter of system", Company_id = _company?.Id, Is_active = true };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/sector/v1", newSector);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<SectorDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Is_active.Should().BeTrue();
            created.Company_id.Should().Be(_company?.Id);
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
            updated.Company_id.Should().Be(_sector?.Company_id);
            updated.Is_active.Should().BeTrue();
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
            foundSector.Company_id.Should().Be(_sector?.Company_id);
            foundSector.Is_active.Should().BeTrue();
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
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var sectors = page?.List;

            sectors.Should().NotBeNull();
            sectors.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
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
