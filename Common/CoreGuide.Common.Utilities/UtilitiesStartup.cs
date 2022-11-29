using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGuide.Common.Entities.ConfigurationSettings;
using CoreGuide.Common.Utilities.OutputFiller;
using CoreGuide.Common.Utilities.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreGuide.Common.Utilities
{
    public static class UtilitiesStartup
    {
        public static void AddUtilitiesServices(this IServiceCollection services)
        {
            services.AddScoped<IUtilities, Utilities.Utilities>();
            services.AddScoped<IOutputFiller, OutputFiller.OutputFiller>();
        }

        public static void AddJWTAuthorizationServices(this IServiceCollection services,IConfiguration configuration)
        {
            var accessTokenSettings = configuration.GetSection(Strings.ConfigurationsSections.AccessTokenSettings).Get<AccessTokenSettings>();
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
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Strings.Policies.Admin, policy => policy.RequireClaim(Strings.JWTClaimNames.Role, Strings.Roles.Admin));
                options.AddPolicy(Strings.Policies.User, policy => policy.RequireClaim(Strings.JWTClaimNames.Role, Strings.Roles.User));
            });
        }

        private static SecurityKey GetSecurityKey(string secretKey)
        {
            byte[] SymmetricKey = Convert.FromBase64String(secretKey);
            return new SymmetricSecurityKey(SymmetricKey);
        }

    }
}
