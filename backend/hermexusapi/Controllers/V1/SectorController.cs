using hermexusapi.DTO.V1;
using hermexusapi.Hypermedia.Utils;
using hermexusapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hermexusapi.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]/v1")]
    [Authorize("Bearer")]
    public class SectorController(
        ISectorService sectorService, ILogger<SectorController> logger) : ControllerBase
    {
        private readonly ISectorService _sectorService = sectorService;
        private readonly ILogger<SectorController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(SectorDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] SectorDTO sector)
        {
            _logger.LogInformation($"Creating new sector {sector.Name}");
            var sectorCreated = _sectorService.Create(sector);
            if (sectorCreated == null)
            {
                _logger.LogError($"Failed to create sector {sector.Name}");
                return NotFound();
            }
            _logger.LogDebug($"Sector {sector.Name} created successfully");
            return Ok(sectorCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(SectorDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching sector with ID {id}", id);
            var sector = _sectorService.FindById(id);
            if (sector == null)
            {
                _logger.LogWarning("Sector with ID {id} not found", id);
                return NotFound();
            }
            return Ok(sector);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(SectorDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] SectorDTO sector)
        {
            _logger.LogInformation("Updating sector with ID {id}", sector.Id);
            var updatedSector = _sectorService.Update(sector);
            if (updatedSector == null)
            {
                _logger.LogError("Failed to update sector with ID {id}", sector.Id);
                return NotFound();
            }
            _logger.LogDebug("Sector with ID {id} updated successfully", sector.Id);
            return Ok(updatedSector);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(SectorDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting sector with ID {id}", id);
            if (_sectorService.Delete(id) == false)
            {
                _logger.LogWarning("Sector with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("Sector with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<SectorDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching sectors with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_sectorService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(204, Type = typeof(SectorDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {
            _logger.LogInformation($"Disabling sector with ID {id}");
            var disabledPerson = _sectorService.Disable(id);
            if (disabledPerson == null)
            {
                _logger.LogError($"Failed to disable sector with ID {id}");
                return NotFound();
            }
            _logger.LogDebug($"Person with ID {id} disabled successfully");
            return Ok(disabledPerson);
        }
    }
}
