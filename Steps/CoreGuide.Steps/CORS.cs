using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class CORS
    {

        #region Steps
        #region Default policy
        /// 1) Add cors services and register default policy
        const string corsExample = @"services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(policy =>
                {
                    var allowedOrigins = Configuration.GetSection(APIStrings.ConfigurationsSections.AllowedOrigins).Get<string[]>();
                    policy.WithOrigins(allowedOrigins)
                          .WithHeaders(""*"")   ///.AllowAnyHeader()
                          .WithMethods(""*"");  ///.AllowAnyMethod()
                          
                });
            });";
        /// 2) configue using  CORS middleware in Configue method "app.UseCors();"
        /// 3) You can specific CORs to certian endpoints in endpoint rounting
        const string corsExample2 = @"endpoints.MapControllers()
             .RequireCors(MyAllowSpecificOrigins);";

        /// Supposedly the cors can initiate preflight OPTIONS request before the original one to see allowed origins,methods and headers.
        /// Example:
        /// Access-Control-Allow-Headers: Content-Type,x-custom-header | "*"
        /// Access-Control-Allow-Methods: PUT,DELETE,GET,OPTIONS | "*"
        /// Access-Control-Allow-Origin: https://cors1.azurewebsites.net | "*"
        /// AllowAnyHeader and AllowAnyMethod set the headers to anything instead of "*"
        #endregion

        #region Named policy
        /// Named policies may be used to use specific policies for specific resources, 
        /// 1) just use AddPolicy instead of AddDefaultPolicy and give it a name before the policy configuration
        /// 2) pass the name to app.useCors("name");
        #endregion

        #region Using attibute
        /// the attribute can be placed on Controller or Actions
        /// 1) [EnableCors] used to enable CORS, and it can take specific policy name.
        /// 2) [DisableCors] used for the opposite, but [DisableCors] attribute does not disable CORS that has been enabled by endpoint routing with RequireCors.
        #endregion
        #endregion

        #region References
        /// https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0#how-cors
        /// https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS#functional_overview
        /// https://www.youtube.com/watch?v=vP--GB5ZEMU
        #endregion
    }
}
