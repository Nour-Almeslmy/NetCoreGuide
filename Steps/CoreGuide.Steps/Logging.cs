using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Logging
    {

        #region Serilog


        #region Steps
        /// 1) Install nuget packages (Serilog.AspNetCore - Serilog.Sinks.Async - Serilog.Enrichers.ClientInfo), 
        /// 2) Configure the source of app settings in Program.cs
        const string LogConfigurations = @"var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(""appsettings.json"",false,true)
                .AddJsonFile($""appsettings.{Environment.GetEnvironmentVariable(""ASPNETCORE_ENVIRONMENT"") ?? ""Production""}.json"", true,true)
                .Build();

        Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();";
        /// 3) Create Serilog section in appsettings
            #region Configure sub sections
            /// 1) Using ==> array of used packages
            /// 2) MinimumLevel ==> set default logging level
            /// 3) Override ==> What to write using serilog from other sources.
            /// 4) WriteTo ==> configure the logging target  
            /// 5) "Write:Async" or "Name":"Async" in "WriteTo" section to use asyncronous logging
            /// 6) Args, configure in ["Name":"Async"] ==> used in async logging to configure logging targets
            /// 7) Name ==> write the target type ex: console, file.
            /// 8) Enrich ==> array of enrichers
            /// 9) Properties ==> json of properties
            #endregion
        #endregion

        #region Enrichers
        /// They are used to add more data (enrich) to logs,ex: Serilog.Enrichers.ClientInfo which can add ClientIp and ClientAgent
        #endregion

        #region Properties
        /// Adding custom properties to the logs.
        #endregion
        #endregion
    }
}
