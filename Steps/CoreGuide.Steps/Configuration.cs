using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Configuration
    {

        #region Registering config files
        /// by default appsettings.json is registered by this method " Host.CreateDefaultBuilder(args)" in program.cs
        /// To add new confige file, chain the following method
        const string newFileExample = @"
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile(path:'path',optional:true,reloadOnChange:true);
            })
            ";
        #endregion

        #region Read values

        #region Read directly from IConfiguration service
        /// you cand read directly from the IConfiguration service with square brackets
        /// If there is subkey, you just add colon then the subkey
        const string configExample = @"
            Configuration['key:subKey']
            ";
        #endregion

        #region Options pattern
        /// 1) You make a class for the settings, and its properties have the exact name of the keys
        /// 2) you register it in the services bu registering the class against the section name in the configuration as follows:
        const string configExample2 = @"
             services.Configure<IdentitySettings>(configuration.GetSection(APIStrings.ConfigurationsSections.IdentitySettings));
            ";
        /// 3) To use it, inject the generic options interface in the required class
        const string configExample3 = @"
            IOptions<IdentitySettings> identitysettings
            ";

        /// 4) the configuration class is the value of IOptions
        const string configExample4 = @"
            _identitysettings = identitysettings.Value;
            ";

        /// Notes: 
        /// IOptions gets a singleton instance.values will not be changed upon changes
        /// IOptionsSnapshot gets an instance per request, if json file is configured to reload on changes But it is registered as Scoped and therefore cannot be injected into a Singleton service.
        /// IOptionsMonitor is registered as a Singleton and can be injected into any service lifetime, at the same time it gets real time values

        #endregion

        #endregion

        #region References
        /// https://referbruv.com/blog/posts/working-with-options-pattern-in-aspnet-core-the-complete-guide
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
        #endregion
    }
}
