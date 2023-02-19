using Microsoft.AspNetCore.Mvc;

namespace CybEth.SQLInjectionDemo.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<DevicesController> _logger;

        public DevicesController(ILogger<DevicesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Device> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Device
            {
                Id = index,
                Name = Random.Shared.Next().ToString(),
                Price = Random.Shared.Next().ToString()
            })
            .ToArray();
        }
    }
}