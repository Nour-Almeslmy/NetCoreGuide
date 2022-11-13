using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : ControllerBase
    {
        static HttpClient _client = new HttpClient();

        [HttpGet("BadDotNet")]
        public IActionResult BadDotNet()
        {
            var result = BadExample();
            return Ok(result);
        }

        string BadExample()
        {
            var message = BadGet().GetAwaiter().GetResult();
            return message;
        }

        async Task<string> BadGet()
        {
            var result = await _client.GetStringAsync("https://www.google.com");
            return result;
        }
    }
}
