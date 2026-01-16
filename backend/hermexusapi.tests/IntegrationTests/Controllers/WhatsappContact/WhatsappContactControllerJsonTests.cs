using FluentAssertions;
using hermexusapi.DTO.V1;
using hermexusapi.Models;
using hermexusapi.tests.IntegrationTests.Tools;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace hermexusapi.tests.IntegrationTests.Controllers.WhatsappContact
{
    [TestCaseOrderer(
        TestConfig.TestCaseOrdererFullName,
        TestConfig.TestCaseOrdererAssembly
        )]
    public class WhatsappContactControllerJsonTests : IClassFixture<MySQLFixture>
    {
        private readonly HttpClient _httpClient;
        private readonly MySQLFixture _fixture;
        private static WhatsappContactDTO? _whatsappContact;

        public WhatsappContactControllerJsonTests(MySQLFixture sqlFixture)
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

        [Fact(DisplayName = "01 - Create WhatsappContact")]
        [TestPriority(1)]
        public async Task CreateWhatsappContact()
        {
            // Arrange
            var newWhatsappContact = new WhatsappContactDTO { 
                Name = "Test Contact",
                Phone_number = "11900000000",
                Wa_id = "000000000000000"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/api/whatsapp_contact/v1", newWhatsappContact);

            // Assert
            response.EnsureSuccessStatusCode();
            var created = await response.Content
                .ReadFromJsonAsync<WhatsappContactDTO>();
            created.Should().NotBeNull();
            created.Id.Should().BeGreaterThan(0);
            created.Name.Should().Be("Test Contact");
            created.Phone_number.Should().Be("11900000000");
            created.Wa_id.Should().Be("000000000000000");
            _whatsappContact = created;
        }

        [Fact(DisplayName = "02 - Update WhatsappContact")]
        [TestPriority(2)]
        public async Task Test_UpdateWhatsappContact()
        {
            // Arrange
            _whatsappContact?.Name = "Test Contact updated";
            _whatsappContact?.Phone_number = "11988888888";
            _whatsappContact?.Wa_id = "111111111111111";
            // Act
            var response = await _httpClient
                .PutAsJsonAsync("/api/whatsapp_contact/v1", _whatsappContact);

            // Assert
            response.EnsureSuccessStatusCode();

            var updated = await response.Content
                .ReadFromJsonAsync<WhatsappContactDTO>();
            updated.Should().NotBeNull();
            updated.Id.Should().Be(_whatsappContact?.Id);
            updated.Name.Should().Be(_whatsappContact?.Name);
            updated.Phone_number.Should().Be(_whatsappContact?.Phone_number);
            updated.Wa_id.Should().Be(_whatsappContact?.Wa_id);
            _whatsappContact = updated;
        }

        [Fact(DisplayName = "03 - Find WhatsappId by ID")]
        [TestPriority(3)]
        public async Task Test_FindWhatsappContactById()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync($"/api/whatsapp_contact/v1/{_whatsappContact?.Id}");
            // Assert
            response.EnsureSuccessStatusCode();
            var foundWhatsappContact = await response.Content
                .ReadFromJsonAsync<WhatsappContactDTO>();
            foundWhatsappContact.Should().NotBeNull();
            foundWhatsappContact.Id.Should().Be(_whatsappContact?.Id);
            foundWhatsappContact.Name.Should().Be(_whatsappContact?.Name);
            foundWhatsappContact.Phone_number.Should().Be(_whatsappContact?.Phone_number);
            foundWhatsappContact.Wa_id.Should().Be(_whatsappContact?.Wa_id);
        }

        [Fact(DisplayName = "04 - Find All WhatsappContact")]
        [TestPriority(4)]
        public async Task Test_FindAllWhatsappContact()
        {
            // Arrange & Act
            var response = await _httpClient
                .GetAsync("/api/whatsapp_contact/v1/asc/10/1");
            // <-- sortDirection=asc, pageSize=10, page=1
            // Assert
            response.EnsureSuccessStatusCode();

            var page = await response.Content
                .ReadFromJsonAsync<PagedSearch<WhatsappContactDTO>>();
            page.Should().NotBeNull();
            page.Current_page.Should().Be(1);
            page.Page_size.Should().Be(10);

            var WhatsappContacts = page?.List;

            WhatsappContacts.Should().NotBeNull();
            WhatsappContacts.Count.Should().BeGreaterThan(0);

            page!.Current_page.Should().BeGreaterThan(0);
            page.Total_results.Should().BeGreaterThan(0);
            page.Page_size.Should().BeGreaterThan(0);
            page.Sort_directions.Should().NotBeNull();
        }

        [Fact(DisplayName = "05 - Delete WhatsappContact")]
        [TestPriority(5)]
        public async Task Test_DeleteRole()
        {
            // Arrange & Act
            var response = await _httpClient
                .DeleteAsync($"/api/whatsapp_contact/v1/{_whatsappContact?.Id}");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
