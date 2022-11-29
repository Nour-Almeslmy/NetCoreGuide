using CoreGuide.API.Utilities.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        private readonly ILogger<LogController> _logger;
        private readonly Serilog.ILogger _serilogger;

        public LogController(
            ILogger<LogController> logger)
        {
            _logger = logger;
            //_serilogger = Serilog.Log.Logger.ForContext<LogController>(Serilog.Events.LogEventLevel.Information, "Filter",this);
            _serilogger = Serilog.Log.Logger.ForContext<LogController>().ForContext("Console","True");
        }

        [HttpGet("logger")]
        public IActionResult Log()
        {
            _logger.LogTrace("Trace msg\r\n");
            _logger.LogDebug("Debug msg\r\n");
            _logger.LogInformation("Information msg\r\n");
            _logger.LogError(new NullReferenceException("Null"), "Error null");
            _logger.LogWarning("Warning msg\r\n");
            return Ok();
        }

        [HttpGet("logEx")]
        public IActionResult LogEx()
        {
            try
            {

                throw new AggregateException("Errooooooooooooooooooooooooooooooooooooooor",
                    new ArgumentNullException("first inner excpetion",
                    new ArgumentException("second exception")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [ServiceFilter(typeof(SerilogFilterAttribute))]
        //[TypeFilter(typeof(SerilogFilterAttribute),Arguments = new object[] {"Action"})]
        [HttpGet("serlilog")]
        public IActionResult SerilogTest()
        {
            _serilogger.Error(new NullReferenceException("Serilog Null"), "Serilog Error null");
            _serilogger.Debug("Serilog Debug");
            return Ok();
        }
    }
}
