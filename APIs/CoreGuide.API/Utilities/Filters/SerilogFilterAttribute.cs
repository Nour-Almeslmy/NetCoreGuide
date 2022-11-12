using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
//using Serilog;
using System;
using System.Threading.Tasks;

namespace CoreGuide.API.Utilities.Filters
{
    public class SerilogFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<SerilogFilterAttribute> _logger;
        private readonly string _name;

        public SerilogFilterAttribute(Microsoft.Extensions.Logging.ILogger<SerilogFilterAttribute> logger,string name = "global")
        {
            _logger = logger;
            _name = name;
            //_logger = Log.Logger.ForContext<SerilogFilterAttribute>(Serilog.Events.LogEventLevel.Information,"Filter",this);
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"Before {_name} Filter");
            await next();
            _logger.LogInformation($"After {_name} Filter");
        }
    }
}
