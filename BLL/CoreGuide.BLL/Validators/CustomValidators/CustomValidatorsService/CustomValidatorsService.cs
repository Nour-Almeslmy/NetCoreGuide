using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.Common.Utilities.Utilities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators.CustomValidators
{
    internal class CustomValidatorsService : ICustomValidatorsService
    {
        private readonly IdentitySettings _identitySettings;
        private readonly IUtilities _utilities;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        //private CancellationToken _cancellationToken => CancellationToken.None;

        public CustomValidatorsService(
            IOptionsSnapshot<IdentitySettings> identitySettings,
            IUtilities utilities,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _identitySettings = identitySettings.Value;
            _utilities = utilities;
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool ValidatePassword(string password)
        {
            bool isValid = false;
            if (_identitySettings.PassowrdSettings.RequireDigit && !password.Any(char.IsDigit)) return isValid;
            if (_identitySettings.PassowrdSettings.RequireLowercase && !password.Any(char.IsLower)) return isValid;
            if (_identitySettings.PassowrdSettings.RequireUppercase && !password.Any(char.IsUpper)) return isValid;
            if (_identitySettings.PassowrdSettings.RequireNonAlphanumeric && !password.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))) return isValid;
            if (_identitySettings.PassowrdSettings.RequiredLength > password.Length) return isValid;
            if (_identitySettings.PassowrdSettings.RequiredUniqueChars > password.Count(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))) return isValid;
            isValid = true;
            return isValid;
        }


        public Task<bool> IsCurrentPasswordCorrect(string password)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return _employeeRepository.CheckPasswordAsync(userName,password);
        }

        public bool IsDialValid(string dial)
        {
            return _utilities.IsValidDial(dial);
        }

        public bool IsEmailValid(string email)
        {
            return _utilities.IsValidEmail(email);
        }
        public bool IsValidName(string name)
        {
            return name.Any(char.IsLetter);
        }

        public async Task<bool> DoesDepartmentExist(int id, CancellationToken cancellationToken)
        {
            return await _departmentRepository.DoesDepartmentExistAsync(id,cancellationToken);
        }
        public async Task<bool> DoesManagerExist(Guid id, CancellationToken cancellationToken)
        {
            return await _employeeRepository.DoesEmployeeExistAsync(id,cancellationToken);
        }
        public async Task<bool> DoesEmployeeExist(string userName, CancellationToken cancellationToken)
        {
            return await _employeeRepository.DoesEmployeeExistAsync(userName,cancellationToken);
        }


    }
}
