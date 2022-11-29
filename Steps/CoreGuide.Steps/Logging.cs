using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Logging
    {



        #region Steps
        /// 1) Install nuget packages (Serilog.AspNetCore - Serilog.Sinks.Async - Serilog.Enrichers.ClientInfo), 
        /// 2) Add ".UseSerilog()" to host configuration to set serilog as logging provider
        /// 3) Configure the source of app settings in Program.cs
        const string LogConfigurations = @"var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(""appsettings.json"",false,true)
                .AddJsonFile($""appsettings.{Environment.GetEnvironmentVariable(""ASPNETCORE_ENVIRONMENT"") ?? ""Production""}.json"", true,true)
                .Build();

        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();";
        /// 4) Create Serilog section in appsettings
        #region Configure sub sections
        /// 1) Using ==> array of used packages
        /// 2) MinimumLevel ==> set default logging level
        /// 3) Override ==> What to write using serilog from other sources (Namespaces).
        /// 4) WriteTo ==> configure the logging target  
        /// 5) "Write:Async" or "Name":"Async" in "WriteTo" section to use asyncronous logging
        /// 6) Args, configure in ["Name":"Async"] ==> used in async logging to configure logging targets
        /// 7) Name ==> write the target type ex: console, file.
        /// 8) Enrich ==> array of enrichers
        /// 9) Properties ==> json of properties
        /// 10) Filters ==> Added to filter what to log
        #endregion
        #endregion

        #region Enrichers
        /// They are used to add more data (enrich) to logs,ex: Serilog.Enrichers.ClientInfo which can add ClientIp and ClientAgent
        #endregion

        #region Properties
        /// Adding custom properties to the logs.
        #endregion

        #region Usage
        /// 1) Inject ILogger of Microsoft.Extensions.Logging as you have overriden the system logger
        /// 2) Use "Log.Logger.ForContext" to set the logger, for context is used to add more props especially for this context
        const string forContextExample = " _logger = Log.Logger.ForContext<SerilogFilterAttribute>(Serilog.Events.LogEventLevel.Information,\"Filter\",this);";
        #endregion

        #region References
        /// https://benfoster.io/blog/serilog-best-practices/#standard-serilog-enrichers
        /// https://github.com/serilog/serilog-sinks-file
        /// https://github.com/serilog/serilog/wiki/Formatting-Output
        /// https://github.com/serilog/serilog/wiki/Enrichment
        /// https://github.com/serilog/serilog-settings-configuration
        /// 
        #endregion
    }
}
