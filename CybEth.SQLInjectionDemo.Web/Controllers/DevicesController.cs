using System.Text.Json;
using System.Text.Json.Serialization;
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
        private readonly IConfiguration _config;

        public DevicesController(ILogger<DevicesController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get(string filterQuery)
        {
            try
            {
                if (!this.QueryValid(filterQuery))
                {
                    return BadRequest(JsonSerializer.Serialize(new { Reason = "Invalid query" }));
                }

                var context = new ContosoDbTestContext(this._config["Contoso:DbString"] ?? string.Empty);
                var result = context.Devices.FromSqlRaw<Device>("select * from devices " + filterQuery);
                return Ok(result.ToArray());
            }
            catch
            {
                return StatusCode(500, JsonSerializer.Serialize(new { Reason = "Server Error" }));
            }
        }

        private bool QueryValid(string filterQuery)
        {
            if (filterQuery.Contains("delete"))
            {
                return false;
            }

            return true;
        }
    }
}