using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace my_ip.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpController : ControllerBase
    {
        private readonly ILogger<IpController> _logger;

        public IpController(ILogger<IpController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}