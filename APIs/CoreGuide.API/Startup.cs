using CoreGuide.API.Utilities;
using CoreGuide.API.Utilities.Filters;
using CoreGuide.API.Utilities.Middlewares;
using CoreGuide.BLL.Business;
using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.Common.Entities.ConfigurationSettings;
using CoreGuide.Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

            services.AddControllers(options =>
            {
                // Global registeration
                // options.Filters.Add(new ConsoleLogFilterAttribute());
                // options.Filters.AddService<SerilogFilterAttribute>();
            });
            services.AddTransient<SerilogFilterAttribute>();
            services.AddDataProtection();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
            services.Configure<ForwardedHeadersOptions>(option =>
            {
                option.ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor;
            });
            services.AddCors(opts =>
            {
                opts.AddPolicy("Test", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod());
                
                opts.AddDefaultPolicy(policy =>
                {
                    var allowedOrigins = Configuration.GetSection(APIStrings.ConfigurationsSections.AllowedOrigins).Get<string[]>();
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()   //.AllowAnyHeader()
                        .AllowAnyMethod();  //.AllowAnyMethod()
                });

            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "CoreGuide.API",
                        Version = "v1",
                        Description = "An ASP.NET Core Web API Guide.",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Example Contact",
                            Url = new Uri("https://example.com/contact")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Example License",
                            Url = new Uri("https://example.com/license")
                        }
                    });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // add Security information to each operation for OAuth2, which renders in Swagger-UI as a padlock next to the operation
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                // To show Authorize button
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: `JWT token`",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http
                });

                // to add customer api key in customer header
                /* options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: `Bearer {JWT token}`",
                    In = ParameterLocation.Header,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });*/


                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                         {
                            securityScheme, new List<string>()
                         },
                };
                // The AddSecurityRequirement extension method will add an authorization header to each endpoint when the request is sent.
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
            services.Configure<TempConvertServiceSettings>(Configuration.GetSection(APIStrings.ConfigurationsSections.TempConvertService));
            #endregion

            services.AddLocalizationSettings(Configuration);
            services.AddBusinessServices(connectionString);
            services.AddJWTAuthorizationServices(Configuration);

            services.AddTransient<ConsoleLoggerMiddleWare>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreGuide.API v1"));
            }

            #region custom middlewares
            // app.Use(async (context, next) =>
            // {
            //    Console.WriteLine("Before");
            //    await next();
            //    Console.WriteLine("After");
            // });

            // app.Run(async context => await context.Response.WriteAsync("Terminal delegate")); 
            //app.UseMiddleware<ConsoleLoggerMiddleWare>();

            //app.Map("/Map", MapHandler);
            //app.MapWhen(context => context.Request.Query.ContainsKey("mapwhen"), HandleCustomQuery);
            //app.UseWhen(context => context.Request.Query.ContainsKey("mapwhen"), HandleCustomQuery);
            #endregion

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
                //.RequireCors("Test");
                //endpoints.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
            });

        }

        private void HandleCustomQuery(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Before HandleCustomQuery");
                await next();
                Console.WriteLine("After HandleCustomQuery");
            });
        }

        private void MapHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                Console.WriteLine("MapHandler");
                await context.Response.WriteAsync("map handler");
            }
            );
        }
    }
}
