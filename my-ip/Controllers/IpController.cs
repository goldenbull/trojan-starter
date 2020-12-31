using System;
using Microsoft.AspNetCore.Authorization;
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
        [BasicAuth]
        public string Get()
        {
            var ipstr = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {ipstr}");
            return ipstr;
        }
    }
}