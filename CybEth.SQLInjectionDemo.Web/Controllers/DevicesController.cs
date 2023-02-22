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

        public DevicesController(ILogger<DevicesController> logger)
        {
            _logger = logger;
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

                var context = new ContosoDbTestContext();
                var result = context.Devices.FromSqlRaw<Device>("select * from devices " + filterQuery);
                return Ok(result.ToArray());
            }
            catch
            {
                return BadRequest();
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