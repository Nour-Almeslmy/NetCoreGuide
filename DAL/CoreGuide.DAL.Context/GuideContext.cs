using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Context.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreGuide.DAL.Context
{
    public class GuideContext : IdentityDbContext<Employee, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
                                                         EmployeeUserRole, IdentityUserLogin<Guid>,
                                                         IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public GuideContext(DbContextOptions<GuideContext> options) : base(options)
        {

        }

        #region configure connection string
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder.UseSqlServer("Server=.;Database=CoreGuide;Trusted_Connection=True;"));
        //} 
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
#endif
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Seeding data
            var hrDeprtment = new Department()
            {
                Id = 1,
                Name = "HR"
            };
            var salesDeprtment = new Department()
            {
                Id = 2,
                Name = "Sales"
            };
            builder.Entity<Department>().HasData(hrDeprtment, salesDeprtment);

            #endregion

            #region Applying entity configuration "Using config class"
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            #endregion

            #region Apply entity configuration "Explicit"
            builder.Entity<EmployeeUserRole>()
                .HasKey(eur => new { eur.UserId, eur.RoleId });

            builder.Entity<EmployeeUserRole>()
                .HasOne(eur => eur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmployeeUserRole>()
                .HasOne(eur => eur.Employee)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
        #region DB sets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        #endregion
    }
}
