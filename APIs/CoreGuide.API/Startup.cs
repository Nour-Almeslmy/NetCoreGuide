using CoreGuide.API.Utilities;
using CoreGuide.BLL.Business;
using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.Common.Entities.ConfigurationSettings;
using CoreGuide.Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreGuide.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDataProtection();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.Configure<ForwardedHeadersOptions>(option =>
            {
                option.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor;
            });


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreGuide.API", Version = "v1" });
            });

            AddSolutionServices(services);
        }

        private void AddSolutionServices(IServiceCollection services)
        {
            #region Configurations
            var connectionString = Configuration.GetConnectionString(APIStrings.ConfigurationsSections.ConnectionString);
            services.Configure<IdentitySettings>(Configuration.GetSection(APIStrings.ConfigurationsSections.IdentitySettings));
            services.Configure<AllowedFileSettings>(Configuration.GetSection(APIStrings.ConfigurationsSections.AllowedFileSettings));
            services.Configure<AccessTokenSettings>(Configuration.GetSection(APIStrings.ConfigurationsSections.AccessTokenSettings));
            services.Configure<RefreshTokenSettings>(Configuration.GetSection(APIStrings.ConfigurationsSections.RefreshTokenSettings));
            #endregion

            services.AddBusinessServices(connectionString);
            services.AddJWTAuthorizationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreGuide.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
            });
        }
    }
}
