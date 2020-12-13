using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotUrl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Health Controller Called. Service is alive.");
            return "Service is alive. Please use /swagger for endpoints";
        }
    }
}
