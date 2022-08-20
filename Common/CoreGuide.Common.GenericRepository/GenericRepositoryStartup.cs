using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.Common.GenericRepository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.GenericRepository
{
    public static class GenericRepositoryStartup
    {

        public static void AddGenericRepositoryLayerServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
