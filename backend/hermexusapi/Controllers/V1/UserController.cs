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
    public class UserController(
        IUserService userService,
        ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<UserController> _logger = logger;

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(List<UserDTO>))]
        [ProducesResponseType(400)]
        public IActionResult Create(
            [FromBody] UserDTO user)
        {
            if (user == null)
            {
                _logger.LogError($"Failed to create a user");
                return BadRequest("Invalid client request!");
            }

            user.Is_active = true;
            var result = _userService.Create(user);
            _logger.LogDebug($"User created: {user.Username}");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching user with ID {id}", id);
            var user = _userService.FindById(id);
            if (user == null)
            {
                _logger.LogWarning("User with ID {id} not found", id);
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] UserDTO user)
        {
            _logger.LogInformation("Updating user with ID {id}", user.Id);
            var updatedUser = _userService.Update(user);
            if (updatedUser == null)
            {
                _logger.LogError("Failed to update user with ID {id}", user.Id);
                return NotFound();
            }
            _logger.LogDebug("User with ID {id} updated successfully", user.Id);
            return Ok(updatedUser);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting user with ID {id}", id);
            if (_userService.Delete(id) == false)
            {
                _logger.LogWarning("User with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("User with ID {id} deleted successfully", id);
            return NoContent();
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<UserDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching users with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_userService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(204, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {
            _logger.LogInformation($"Disabling user with ID {id}");
            var disabledPerson = _userService.Disable(id);
            if (disabledPerson == null)
            {
                _logger.LogError($"Failed to disable user with ID {id}");
                return NotFound();
            }
            _logger.LogDebug($"Person with ID {id} disabled successfully");
            return Ok(disabledPerson);
        }

    }
}
