using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class JWT
    {

        #region Steps
        /// 1) Define authentication scheme, which is "JwtBearerDefaults.AuthenticationScheme" and add its services
        const string authenticationExample = @"
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            RequireExpirationTime = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            IssuerSigningKey = GetSecurityKey(accessTokenSettings.SecretKey),
                            ValidIssuer = accessTokenSettings.Issuer,
                            ValidAudience = accessTokenSettings.Audience,
                            ClockSkew = TimeSpan.Zero,
                        };
                    });";
        /// 2) You can authorization policies to determine which type of users can access the resources, policies are good for role based or claims based authentication
        const string authorizationExample = @"
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Strings.Roles.Admin, policy => policy.RequireClaim(Strings.JWTClaimNames.Role, Strings.Roles.Admin));
                options.AddPolicy(Strings.Roles.User, policy => policy.RequireClaim(Strings.JWTClaimNames.Role, Strings.Roles.User));
            });
            ";
        /// 3) Add authorization middlewares 
        const string middleWaresExample = @"
            app.UseAuthentication();
            app.UseAuthorization();
            ";
        /// 4) Create new jwt token against the same validation parameters, see TokenService in business
        /// 5)
        #endregion


        #region References Authorization
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-6.0
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-6.0
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
        #endregion
    }
}
