using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.Common.Utilities;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Context.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Context
{
    public static class ContextStartup
    {
        public static void AddDatabaseLayerServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GuideContext>(options => options.UseSqlServer(connectionString));

            var provider = services.BuildServiceProvider();
            var identitySettings = provider.GetRequiredService<IOptionsSnapshot<IdentitySettings>>().Value;

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = identitySettings.PassowrdSettings.RequireDigit;
                options.Password.RequireLowercase = identitySettings.PassowrdSettings.RequireLowercase;
                options.Password.RequireNonAlphanumeric = identitySettings.PassowrdSettings.RequireNonAlphanumeric;
                options.Password.RequireUppercase = identitySettings.PassowrdSettings.RequireUppercase;
                options.Password.RequiredLength = identitySettings.PassowrdSettings.RequiredLength;
                options.Password.RequiredUniqueChars = identitySettings.PassowrdSettings.RequiredUniqueChars;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(identitySettings.LockoutSettings.LockoutTimeInMinutes);
                options.Lockout.MaxFailedAccessAttempts = identitySettings.LockoutSettings.MaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identitySettings.LockoutSettings.AllowedForNewUsers;

                // User settings
                options.User.RequireUniqueEmail = identitySettings.UserSettings.RequireUniqueEmail;
            });

            services
                .AddIdentityCore<Employee>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<GuideContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<ApplicationUserManager>();
                //.AddUserStore<UserStore<Employee, IdentityRole<Guid>, GuideContext, Guid, IdentityUserClaim<Guid>, EmployeeUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>>();
                //.AddRoleStore<RoleStore<IdentityRole<Guid>, GuideContext, Guid, EmployeeUserRole, IdentityRoleClaim<Guid>>>();
            //.AddRoleStore<RoleStore<IdentityRole<Guid>, GuideContext, Guid, EmployeeUserRole, IdentityRoleClaim<Guid>>>();

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));


            #region Add roles if not existing
            AddRoles(services);
            #endregion
        }

        private static void AddRoles(IServiceCollection services)
        {
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var rolesExisted = roleManager.Roles.Any();

            if (!rolesExisted)
            {
                roleManager.CreateAsync(new IdentityRole<Guid> { Name = Strings.Roles.Admin }).Wait();
                roleManager.CreateAsync(new IdentityRole<Guid> { Name = Strings.Roles.User }).Wait();
            }
        }
    }
}
