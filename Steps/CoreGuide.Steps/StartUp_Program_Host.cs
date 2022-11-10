using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class StartUp_Program_Host
    {
        #region Program
        /// It is the entry point of the application, it configure the host and the used startup class
        #endregion

        #region Start up
        /// The StartUp class is called by the program when the host is being created, it is by convention ( not implementing interface)
        /// You can create startup for each environment, but you need to change "ASPNETCORE_ENVIRONMENT" in launchSettings.
        /// ConfigureServices method used to Use this method to add services to the container.
        /// Configure method used to onfigure the HTTP request pipeline. 
        #endregion

        #region Host
        /// The host is responsible for app startup and lifetime management. At a minimum, the host configures a server and a request processing pipeline. The host can also set up logging, dependency injection, and configuration.
        /// The host class are two types(generic / web ==> for backward compatibility with older core versions)
        /// Generic Host, which is the recommended choice now. The Generic Host allows other types of apps (e.g. non-web scenarios) to use cross-cutting framework extensions, such as logging, dependency injection (DI), configuration and app lifetime management
        /// Web Host, which is still available only for backwards compatibility

        /// The host encapsulates all of the application resources (http implementations/ middle wares/ logging / DI / services needed to the start up class
        /// The host injects Configuration to the start up class (IConfiguration - IServiceCollection - IApplicationBuilder - IWebHostEnvironment)
        /// You can remove startup class and inject it in program.cs after creating web builder in webBuilder.ConfigureServices([ConfigureServicesMethod]).Configure([Configure])
        /// Two types of host processes (In process vs. out of process)

        /// In process ==> Uses IIS, on windows server, best performance compared to outOfProcss. Only one app pool per app , app runs inside of the IIS worker process (w3wp.exe),(default with visual studio)
        /// Out of process ==> Used with iis/Apache/NgNix [optional/reverse proxy] + Kestrel), used for cross platform hosting (outside IIS server, that's why it is out or process). (default with command line)
        /// Kestrel ==> Kestrel is a cross-platform web server for ASP.NET Core. Kestrel is the web server that's included and enabled by default in ASP.
        #endregion


        #region References
        /// https://www.linkedin.com/pulse/net-core-startup-class-generic-host-orestis-meikopoulos/
        /// https://www.telerik.com/blogs/understanding-asp-net-core-initialization
        /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-6.0
        /// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0
        /// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/in-process-hosting?view=aspnetcore-6.0
        /// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/out-of-process-hosting?view=aspnetcore-6.0
        #endregion


    }
}
