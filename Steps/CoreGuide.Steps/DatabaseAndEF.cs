using CoreGuide.DAL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class DatabaseAndEF
    {
        #region Code first

        #region Required DAL context packages
        // In DAL Context project, install the following packages:
        /// 1) Microsoft.AspNetCore.Identity.EntityFrameworkCore ==> to use EF core
        /// 2) Microsoft.EntityFrameworkCore.SqlServer ==> to connect to SQL
        /// 3) Microsoft.EntityFrameworkCore.Tools ==> to use nuget manager commands 
        /// 4) Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore ==> it adds middleware to detect ef core error pages
        #endregion

        #region Required web api packages
        // In Web API project, install the following packages:
        /// 1) Microsoft.EntityFrameworkCore.Design
        #endregion

        #region Add connection string in appsettings
        /// Add Connection string in appsettings.json with any name in a section called "ConnectionStrings"
        #endregion

        #region Read Connection string
        /// Read the required connection string in startup services:
        const string ConfiguringConnectionStringInStartUp =
            @" 
            var connectionString = configuration.GetConnectionString(ContextStrings.ConnectionStringSectionName); ==> read section name
            services.AddDbContext<GuideContext>(options => options.UseSqlServer(connectionString)); ==> register the dbcontext as scoped service
            ";
        /// The GuideContext class must expose a public constructor with a DbContextOptions<GuideContext> parameter. This is how context configuration from AddDbContext is passed to the DbContext.

        // Example:
        public void ReadConnectionStringExample(IServiceCollection services)
        {
            /// Check the code here
            ContextStartup.AddDatabaseLayerServices(services, "connectionString");
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// OR
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// read it in the Context class
        const string ConfiguringConnectionStringInContext = 
            @"
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer('Server=.;Database=CoreGuide;Trusted_Connection=True;'));
            } 
            ";
        // Example:
        // Check  CoreGuide.DAL.Context.GuideContext
        #endregion

        #region Add seperate class for entity configuration
        // To add seperate configuration class:
        /// 1) create new class
        /// 2) inherit from IEntityTypeConfiguration<EntityClass>
        /// 3) Implement the interface public void Configure(EntityTypeBuilder<EntityClass> builder)
        /// 4) add it in OnModelCreating method in the Context class as following "builder.ApplyConfiguration(new ConfigurationClass());"

        // Example:
        // Check  CoreGuide.DAL.Context.EntitiesConfigurations.RefreshTokenConfiguration
        #endregion

        #region Add migrations
        /// 1) Make the API project as start up project
        /// 2) Choose the context project in package manager console, then run the following commands
        /// 3) Add-migration <migrationName>
        /// 4) update-database
        #endregion

        #region Not to do before migrations
        /// 1) Can't add roles or use any store managers before adding migration
        /// For example don't do this:
        const string badExample = @"
            roleManager.CreateAsync(new IdentityRole<Guid> { Name = Strings.Roles.Admin }).Wait();
            roleManager.CreateAsync(new IdentityRole<Guid> { Name = Strings.Roles.User}).Wait();";
        #endregion

        #endregion

        #region User manager
        /// UserManager handles cancellation internally, and gets the cancellation token from HttpContext.RequestAborted.
        /// As such, you don't need to pass in a cancellation token, and that's why the methods don't accept one.

        #endregion

        #region Shadow properties
        /// reference: https://docs.microsoft.com/en-us/ef/core/modeling/shadow-properties
        // reference: https://www.entityframeworktutorial.net/efcore/shadow-property.aspx
        #endregion

        #region References
        /// reference: https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/#the-dbcontext-lifetime
        /// Identity stores : http://medium.com/@xsoheilalizadeh/asp-net-core-identity-deep-dive-stores-e0e54291b51d
        #endregion
    }
}
