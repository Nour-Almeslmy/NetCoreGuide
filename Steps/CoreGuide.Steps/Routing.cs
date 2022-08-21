using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Routing
    {
        #region steps to enable routing

        #region Add routing middleware
        /// In Configure method in startup add routing middleware
        const string RoutingMiddleware = @"
            app.UseRouting();
            ";
        #endregion


        #region configure endpoints mapping
        /// app.UseEndpoints is used to configure the routing template
        /// 
        #region Attribute routing
        /// Action methods on controllers annotated with ApiControllerAttribute [ApiController] must be attribute routed.'
        /// The RouteAttribute is used above the Controller class name ex: [Route("api/[controller]")]
        /// You can provide template as well above each method along with the http method ex:[HttpGet("all")] or add Action token replacement in main RouteAttribute ex: [Route("api/[controller]/[action]")]
        /// to map the controllers paths to be used in routing, MapControllers() method must be used
        const string RoutingMiddleware2 = @"
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });";

        /// You can provide two routing paths by adding another RouteAttribute
        /// Action methods on controllers annotated with ApiControllerAttribute must be attribute routed, 
        /// so the actions are inaccessible via conventional routes defined in UseMvc or by UseMvcWithDefaultRoute in Startup.Configure.
        #endregion

        #endregion

        #endregion

        #region References
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-3.1#routing-concepts-1
        #endregion
    }
}
