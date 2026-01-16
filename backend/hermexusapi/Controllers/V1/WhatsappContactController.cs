using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hermexusapi.Controllers.V1
{
    [ApiController]
    [Route("api/whatsapp_contact/v1")]
    [Authorize("Bearer")]
    public class WhatsappContactController(
        IWhatsappContactService whatsappContactService, ILogger<WhatsappContactController> logger) : ControllerBase
    {
        private readonly IWhatsappContactService _whatsappContactService = whatsappContactService;
        private readonly ILogger<WhatsappContactController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(WhatsappContactDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] WhatsappContactDTO whatsapp_contact)
        {
            _logger.LogInformation($"Creating new whatsapp_contact {whatsapp_contact.Name}");
            var whatsapp_contactCreated = _whatsappContactService.Create(whatsapp_contact);
            if (whatsapp_contactCreated == null)
            {
                _logger.LogError($"Failed to create whatsapp_contact {whatsapp_contact.Name}");
                return NotFound();
            }
            _logger.LogDebug($"WhatsappContact {whatsapp_contact.Name} created successfully");
            return Ok(whatsapp_contactCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(WhatsappContactDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching whatsapp_contact with ID {id}", id);
            var whatsapp_contact = _whatsappContactService.FindById(id);
            if (whatsapp_contact == null)
            {
                _logger.LogWarning("WhatsappContact with ID {id} not found", id);
                return NotFound();
            }
            return Ok(whatsapp_contact);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(WhatsappContactDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] WhatsappContactDTO whatsapp_contact)
        {
            _logger.LogInformation("Updating whatsapp_contact with ID {id}", whatsapp_contact.Id);
            var updatedWhatsappContact = _whatsappContactService.Update(whatsapp_contact);
            if (updatedWhatsappContact == null)
            {
                _logger.LogError("Failed to update whatsapp_contact with ID {id}", whatsapp_contact.Id);
                return NotFound();
            }
            _logger.LogDebug("WhatsappContact with ID {id} updated successfully", whatsapp_contact.Id);
            return Ok(updatedWhatsappContact);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(WhatsappContactDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting whatsapp_contact with ID {id}", id);
            if (_whatsappContactService.Delete(id) == false)
            {
                _logger.LogWarning("WhatsappContact with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("WhatsappContact with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<WhatsappContactDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching whatsapp_contacts with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_whatsappContactService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }
    }
}
