using CoreGuide.DAL.Repository.Respositories;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository
{
    public static class RepositoryStartup
    {
        public static void AddRepositoryLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOTPRespository, OTPRespository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IGuideUnitOfWork, GuideUnitOfWork>();
        }
    }
}
