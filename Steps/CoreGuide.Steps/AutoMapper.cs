using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Steps
{
    internal class AutoMapper
    {
        #region Steps
        /// 1) Install package "AutoMapper.Extensions.Microsoft.DependencyInjection"
        /// 2) add automapper services for the running assembly, where it register them as transient except for MapperConfiguration which registers it as singleton
        const string services = @"
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            ";

        /// 3) Create class for each manager as a profile and inherit from Profile class of automapper
        /// 4) Configure in each profile the mapping
        const string mappingConfiguration = @"
             CreateMap<RegisterUserInput, Employee>()
                .ForMember(e => e.PhoneNumber, opts => opts.MapFrom(r => r.Dial));
            ";
        /// 5) Inject IMapper service from automapper where you want to map
        const string mapping = @"
            _mapper.Map<Employee>(input);
            ";
        #endregion

        #region Reference
        /// https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection
        /// https://docs.automapper.org/en/stable/Dependency-injection.html
        /// https://docs.automapper.org/en/stable/Configuration.html#profile-instances
        #endregion
    }
}
