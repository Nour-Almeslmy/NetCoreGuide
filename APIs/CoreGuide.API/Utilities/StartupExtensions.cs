using CoreGuide.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace CoreGuide.API.Utilities
{
    public static class StartupExtensions
    {
        public static void AddLocalizationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(APIStrings.ConfigurationsSections.Localization).Get<LocalizationSettings>();
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>();
                foreach (var culture in settings.SupportedCultures)
                {
                    supportedCultures.Add(new CultureInfo(culture));
                };
                options.DefaultRequestCulture = new RequestCulture(settings.DefaultCulture, settings.DefaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
                /*options.RequestCultureProviders = new[] { new CustomRequestCultureProvider(async context =>
                {
                    var value = context.Request.Headers["lang"].ToString();
                    return new ProviderCultureResult(value);    
                }
                ) 
                };*/
            });
        }
    }
}
