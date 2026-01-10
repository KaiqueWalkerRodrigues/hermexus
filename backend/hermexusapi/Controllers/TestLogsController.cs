using Microsoft.AspNetCore.Mvc;

namespace hermexusapi.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestLogsController(ILogger<TestLogsController> logger) : ControllerBase
    {
        private readonly ILogger<TestLogsController> _logger = logger;

        [HttpGet("log-test")]
        public IActionResult LogTest()
        {
            _logger.LogTrace("This is a TRACE log message");
            _logger.LogDebug("This is a DEBUG log message.");
            _logger.LogInformation("This is an INFORMATION log message.");
            _logger.LogWarning("This is a WARNING log message.");
            _logger.LogError("This is an ERROR log message.");
            _logger.LogCritical("This is a CRITICAL log message.");
            return Ok("Log messages have been generated. Check your logging output.");
        }
    }
}
