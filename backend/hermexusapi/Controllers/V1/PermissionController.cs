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
    public class PermissionController(
        IPermissionService permissionService, ILogger<PermissionController> logger
        ) : ControllerBase
    {
        private readonly IPermissionService _permissionService = permissionService;
        private readonly ILogger<PermissionController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PermissionDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] PermissionDTO permission)
        {
            _logger.LogInformation($"Creating new permission {permission.Name}");
            var permissionCreated = _permissionService.Create(permission);
            if (permissionCreated == null)
            {
                _logger.LogError($"Failed to create permission {permission.Name}");
                return NotFound();
            }
            _logger.LogDebug($"Permission {permission.Name} created successfully");
            return Ok(permissionCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PermissionDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching permission with ID {id}", id);
            var permission = _permissionService.FindById(id);
            if (permission == null)
            {
                _logger.LogWarning("Permission with ID {id} not found", id);
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PermissionDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] PermissionDTO permission)
        {
            _logger.LogInformation("Updating permission with ID {id}", permission.Id);
            var updatedPermission = _permissionService.Update(permission);
            if (updatedPermission == null)
            {
                _logger.LogError("Failed to update permission with ID {id}", permission.Id);
                return NotFound();
            }
            _logger.LogDebug("Permission with ID {id} updated successfully", permission.Id);
            return Ok(updatedPermission);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(PermissionDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting permission with ID {id}", id);
            if (_permissionService.Delete(id) == false)
            {
                _logger.LogWarning("Permission with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("Permission with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<PermissionDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching permissions with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_permissionService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }
    }
}
