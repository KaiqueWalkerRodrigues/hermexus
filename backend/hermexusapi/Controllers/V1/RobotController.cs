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
    public class RobotController(
        IRobotService robotService, ILogger<RobotController> logger) : ControllerBase
    {
        private readonly IRobotService _robotService = robotService;
        private readonly ILogger<RobotController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RobotDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] RobotDTO robot)
        {
            _logger.LogInformation($"Creating new robot {robot.Name}");
            var robotCreated = _robotService.Create(robot);
            if (robotCreated == null)
            {
                _logger.LogError($"Failed to create robot {robot.Name}");
                return NotFound();
            }
            _logger.LogDebug($"Role {robot.Name} created successfully");
            return Ok(robotCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RobotDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching robot with ID {id}", id);
            var robot = _robotService.FindById(id);
            if (robot == null)
            {
                _logger.LogWarning("Role with ID {id} not found", id);
                return NotFound();
            }
            return Ok(robot);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(RobotDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] RobotDTO robot)
        {
            _logger.LogInformation("Updating robot with ID {id}", robot.Id);
            var updatedRole = _robotService.Update(robot);
            if (updatedRole == null)
            {
                _logger.LogError("Failed to update robot with ID {id}", robot.Id);
                return NotFound();
            }
            _logger.LogDebug("Role with ID {id} updated successfully", robot.Id);
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(RobotDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting robot with ID {id}", id);
            if (_robotService.Delete(id) == false)
            {
                _logger.LogWarning("Role with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("Role with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<RobotDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching robots with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_robotService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }
    }
}
