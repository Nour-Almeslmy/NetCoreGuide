using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class Localization
    {


        /// You can use "Accept-Language" header
        /// Steps:
        /// 1) In ConfigureServices in StartUp, add " services.AddLocalization();"
        /// 2) Configure "RequestLocalizationOptions" for the following:
        ///     a) What are the supported cultures
        ///     b) What is the default culture
        ///     c) What kind of culture provider you can use 
        ///     { 
        ///     AcceptLanguageHeaderRequestCultureProvider: For Accept-Language header,
        ///       QueryStringRequestCultureProvider: for url provider,
        ///       CookieRequestCultureProvider: for cookie provider
        ///       }
        const string localizationSevrice = @"
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
            });";
        ///  3) In Configure in Startup, configure using RequestLocalizationOptions
        const string localizationConfigs = @"
        var requestLocalizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestLocalizationOptions.Value);";

        #region Resources file
        /// You can use resources file directly, nut there is no global resources files
        #endregion

        #region Json Based Localization
        /// 1) Use the same configuration for localization
        /// 2) Create custom localizer class to implement IStringLocalizer. Make sure when using the file path to make the path accessible (on API project).
        /// 3) Add class to implement IStringLocalizerFactory and return instance of your custom localizer class.
        /// 4) For better performance you can use caching by using IDistributedCache. To register the service you cofnigure "services.AddDistributedMemoryCache();" in startup
        /// 5) Use it where you want through injecting IStringLocalizer<ClassName>
        #endregion

        /// References
        /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-6.0

        /// https://www.youtube.com/watch?v=xd4pw02J_6M&list=PL62tSREI9C-e6tydoksWUdUEginpmHmoa&index=1
        /// https://codewithmukesh.com/blog/json-based-localization-in-aspnet-core/
    }
}
