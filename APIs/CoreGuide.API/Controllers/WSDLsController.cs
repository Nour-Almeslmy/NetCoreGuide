using CoreGuide.BLL.Business.Connected_Services.tempConvert;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WSDLsController : ControllerBase
    {
        private readonly ITempService _tempService;

        public WSDLsController(ITempService tempService)
        {
            _tempService = tempService;
        }

        [HttpGet]
        public async Task<IActionResult> FahrenheitToCelsius([FromQuery] string degree)
        {
            var celsius = await _tempService.FahrenheitToCelsiusAsync(degree);
            return Ok(celsius);
        }

        [HttpGet]
        public async Task<IActionResult> CelsiusToFahrenheit([FromQuery] string degree)
        {
            var fehrenheit = await _tempService.CelsiusToFahrenheitAsync(degree);
            return Ok(fehrenheit);
        }
    }
}
