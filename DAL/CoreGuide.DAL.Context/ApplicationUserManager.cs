using CoreGuide.DAL.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Context
{
    public class ApplicationUserManager : UserManager<Employee>
    {

        public ApplicationUserManager(IUserStore<Employee> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Employee> passwordHasher,
            IEnumerable<IUserValidator<Employee>> userValidators, IEnumerable<IPasswordValidator<Employee>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Employee>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, 
                  passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
