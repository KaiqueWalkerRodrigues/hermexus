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
    public class CompanyController(
        ICompanyService companyService, ILogger<CompanyController> logger) : ControllerBase
    {
        private readonly ICompanyService _companyService = companyService;
        private readonly ILogger<CompanyController> _logger = logger;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Create([FromBody] CompanyDTO company)
        {
            _logger.LogInformation($"Creating new company {company.Name}");
            var companyCreated = _companyService.Create(company);
            if (companyCreated == null)
            {
                _logger.LogError($"Failed to create company {company.Name}");
                return NotFound();
            }
            _logger.LogDebug($"Company {company.Name} created successfully");
            return Ok(companyCreated);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindById(long id)
        {
            _logger.LogInformation("Fetching company with ID {id}", id);
            var company = _companyService.FindById(id);
            if (company == null)
            {
                _logger.LogWarning("Company with ID {id} not found", id);
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Update([FromBody] CompanyDTO company)
        {
            _logger.LogInformation("Updating company with ID {id}", company.Id);
            var updatedCompany = _companyService.Update(company);
            if (updatedCompany == null)
            {
                _logger.LogError("Failed to update company with ID {id}", company.Id);
                return NotFound();
            }
            _logger.LogDebug("Company with ID {id} updated successfully", company.Id);
            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _logger.LogInformation("Deleting company with ID {id}", id);
            if (_companyService.Delete(id) == false)
            {
                _logger.LogWarning("Company with ID {id} not found to delete", id);
                return NotFound();
            }
            _logger.LogDebug("Company with ID {id} deleted successfully", id);
            return NoContent();
        }


        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(PagedSearchDTO<CompanyDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult FindWithPagedSearch(
            [FromQuery] string? name,
            string sortDirection,
            int pageSize,
            int page
            )
        {
            _logger.LogInformation(@$"Fetching companies with paged search:
                name={name},
                sortDirection={sortDirection},
                pageSize={pageSize},
                page={page}");
            return Ok(_companyService.FindWithPagedSearch(
                name, sortDirection, pageSize, page));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(204, Type = typeof(CompanyDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Disable(long id)
        {
            _logger.LogInformation($"Disabling company with ID {id}");
            var disabledCompany = _companyService.Disable(id);
            if (disabledCompany == null)
            {
                _logger.LogError($"Failed to disable company with ID {id}");
                return NotFound();
            }
            _logger.LogDebug($"Company with ID {id} disabled successfully");
            return Ok(disabledCompany);
        }
    }
}
