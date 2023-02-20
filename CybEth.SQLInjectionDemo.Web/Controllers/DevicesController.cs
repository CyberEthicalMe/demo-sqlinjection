using CybEth.SQLInjectionDemo.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CybEth.SQLInjectionDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ILogger<DevicesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Device> Get()
        {
            var context = new ContosoDbTestContext();
            var result = context.Devices.FromSqlRaw<Device>("select * from devices");
            return result.ToArray();
        }
    }
}