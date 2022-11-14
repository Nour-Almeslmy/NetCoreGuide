using CoreGuide.BLL.Models.ConfigurationSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly AllowedFileSettings _options;
        private readonly AllowedFileSettings _optionsSnapshot;

        public ConfigurationsController(
            IOptionsSnapshot<AllowedFileSettings> optionsSnapshot,
            IOptions<AllowedFileSettings> options)
        {
            _options = options.Value;
            _optionsSnapshot = optionsSnapshot.Value;
        }


        [HttpGet]
        public IActionResult Config()
        {
            var result = $"IOptions: {_options.MaximumImageSize}\r\nIOptionsSnapshot: {_optionsSnapshot.MaximumImageSize}";
            return Ok(result);
        }
    }
}
