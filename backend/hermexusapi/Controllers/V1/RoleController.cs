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
    public class RoleController(
        IRoleService roleService, ILogger<RoleController> logger) : ControllerBase
    {
        private readonly IRoleService _roleService = roleService;
        private readonly ILogger<RoleController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(RoleDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] RoleDTO role)
        {
            _logger.LogInformation($"Creating new role {role.Name}");
            var roleCreated = _roleService.Create(role);
            if (roleCreated == null)
            {
                _logger.LogError($"Failed to create role {role.Name}");
                return NotFound();
            }
            _logger.LogDebug($"Role {role.Name} created successfully");
            return Ok(roleCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RoleDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching role with ID {id}", id);
            var role = _roleService.FindById(id);
            if (role == null)
            {
                _logger.LogWarning("Role with ID {id} not found", id);
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(RoleDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] RoleDTO role)
        {
            _logger.LogInformation("Updating role with ID {id}", role.Id);
            var updatedRole = _roleService.Update(role);
            if (updatedRole == null)
            {
                _logger.LogError("Failed to update role with ID {id}", role.Id);
                return NotFound();
            }
            _logger.LogDebug("Role with ID {id} updated successfully", role.Id);
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(RoleDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting role with ID {id}", id);
            if (_roleService.Delete(id) == false)
            {
                _logger.LogWarning("Role with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("Role with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<RoleDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching roles with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_roleService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }
    }
}
