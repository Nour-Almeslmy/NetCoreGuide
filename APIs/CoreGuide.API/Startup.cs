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
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
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

            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(policy =>
                {
                    var allowedOrigins = Configuration.GetSection(APIStrings.ConfigurationsSections.AllowedOrigins).Get<string[]>();
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "CoreGuide.API",
                        Version = "v1"
                    });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // add Security information to each operation for OAuth2
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                         {
                            securityScheme, new string[] { }
                         },
                };
                options.AddSecurityRequirement(securityRequirements);
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
            services.AddLocalizationSettings(Configuration);
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
            var requestLocalizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestLocalizationOptions.Value);
            app.UseRouting();
            app.UseCors();
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
