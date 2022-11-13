using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CoreGuide.BLL.Business.Manager.Accounts;
using CoreGuide.BLL.Business.Manager.Department;
using CoreGuide.BLL.Business.Manager.Resources;
using CoreGuide.BLL.Business.Utilities.ContextAccessor;
using CoreGuide.BLL.Business.Utilities.FileUploaderService;
using CoreGuide.BLL.Business.Utilities.GuideFillerService;
using CoreGuide.BLL.Business.Utilities.JSONLocalizer;
using CoreGuide.BLL.Business.Utilities.MapperService;
using CoreGuide.BLL.Business.Utilities.TokenService;
using CoreGuide.BLL.Business.Utilities.ValidationService;
using CoreGuide.BLL.Business.Validators;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.Common.GenericRepository;
using CoreGuide.Common.Utilities;
using CoreGuide.DAL.Context;
using CoreGuide.DAL.Repository;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace CoreGuide.BLL.Business
{
    public static class BusinessStartup
    {
        public static void AddBusinessServices(this IServiceCollection services, string connectionString)
        {
            #region Dependent layers services
            services.AddDatabaseLayerServices(connectionString);
            services.AddGenericRepositoryLayerServices();
            services.AddRepositoryLayerServices();
            services.AddUtilitiesServices();
            #endregion

            #region Utilities
            services.AddScoped<ICustomValidatorsService, CustomValidatorsService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IGuideFillerService, GuideFillerService>();
            services.AddScoped<IMapperService, MapperService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContextAccessor, ContextAccessor>();
            services.AddScoped<IFileUploaderService, OnDiskFileUploaderService>();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            #endregion

            #region Validators
            services.AddFluentValidation(config =>
            {
                config.RegisterValidatorsFromAssemblyContaining<RegisterUserInputValidator>(includeInternalTypes: true);
                config.AutomaticValidationEnabled = false;
            });
            #endregion

            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Managers
            services.AddScoped<IAccountsManager, AccountsManager>();
            services.AddScoped<IResourcesManager, ResourcesManager>();
            services.AddScoped<IDepartmentManager, DepartmentManager>();

            #endregion

        }
    }
}
