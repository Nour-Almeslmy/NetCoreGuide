using Microsoft.AspNetCore.Mvc;

namespace CoreGuide.CoreAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        //[ResponseCache(Duration = 20)]
        [ConsoleFilter("Test filter")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine("Action execution");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(template:"/search/{search:alpha}", Name = "test")]
        public IActionResult Test(string search)
        {
            return Ok(Summaries.FirstOrDefault(t => t == search));
        }

        [HttpPost("Post")]
        public IActionResult Post([FromBody] string weather)
        {
            var obj = new WeatherForecast() { Date = DateTime.Now,Summary = weather,TemperatureC = 44};
            return CreatedAtRoute("test",new {search =obj.Summary },obj);
        }

        [HttpGet("log")]
        public IActionResult Log()
        {
            _logger.LogInformation("info");
            _logger.LogError("error");
            return Ok();
        }
    }
}