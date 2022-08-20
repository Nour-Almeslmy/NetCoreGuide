using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class DependecyInjection
    {
        public void RegisterServicesExamples(IServiceCollection services)
        {

            #region scoped
            /// Interface and Implementation
            const string ScopedServiceRegistartion = @"
                services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
                ";           
            #endregion


            #region Singleton
            /// sigleton configuration model
            const string SigletonConfiguration = @"
                services.AddSingleton<MyClass>();
                ";
            #endregion

            #region Transient

            /// Interface and Implementation
            const string TransientServiceRegistartion = @"
                services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
                ";
            #endregion


            #region DBContext
            /// AddDbContext method is used to register DBContext with SCOPED lifetime as default
            const string ContextRegistrationExample = @"
                services.AddDbContext<GuideContext>(options => options.UseSqlServer(connectionString));
                ";
            #endregion


            #region Configuration
            /// register configuration model that the configurations will bind against
            const string ConfigurationsModel = @"
                services.Configure<FacebookConfigurations>(configuration.GetSection(APIStrings.ConfigurationsSections.Facebook));
                ";

            /// IOptions vs IOptionsSnapshot vs IOptionsMonitor
            #endregion

            #region Httpclient
            /// If you use httpClient instance in the service
            /// It registers the service as Transient
            const string httpRegistartion = @"
                services.AddHttpClient<IFileUploaderService, OnDiskFileUploaderService>();
                ";
            #endregion

            #region Generics
            /// Generics
            const string GenericRegistartion = @"
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                ";
            #endregion

            #region References
            /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
            /// https://khalidabuhakmeh.com/resolve-services-in-aspnet-core-startup
            #endregion
        }

    }
}
