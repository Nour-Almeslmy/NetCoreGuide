using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators
{
    internal class RegisterUserInputValidator : AbstractValidator<RegisterUserInput>
    {
        private readonly ICustomValidatorsService _customValidatorsService;
        private readonly AllowedFileSettings _allowedFileSettings;

        public RegisterUserInputValidator(
            ICustomValidatorsService customValidatorsService,
            IOptionsSnapshot<AllowedFileSettings> allowedFileSettings
            )
        {
            CascadeMode = CascadeMode.Stop;
            _customValidatorsService = customValidatorsService;
            _allowedFileSettings = allowedFileSettings.Value;

            RuleFor(u => u.Dial)
                .Must(_customValidatorsService.IsDialValid)
                .WithErrorCode($"{GuideErrorCodes.InvalidDial}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.DialInvalid);

            RuleFor(u => u.Name)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NameNull)
                .Must(_customValidatorsService.IsValidName)
                .WithErrorCode($"{GuideErrorCodes.InvalidName}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NameInvalid);

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.EmailNull)
                .Must(_customValidatorsService.IsEmailValid)
                .WithErrorCode($"{GuideErrorCodes.InvalidEmail}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.EmailInvalid);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.PasswordNull)
                .Must(_customValidatorsService.ValidatePassword)
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.PasswordInvalid);

            RuleFor(u => u.ConfirmedPassword)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.ConfirmedPasswordNull)
                .Equal(c => c.Password)
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.ConfirmedPasswordInvalid);

            RuleFor(u => u.Salary)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.SalaryInvalid);

            RuleFor(u => u.ProfilePicture)
                .MaximumSize(_allowedFileSettings.MaximumImageSize)
                .AllowedTypes(_allowedFileSettings.AllowedImageExtensions);

            RuleFor(u =>u.DepartmentId)
                .MustAsync( async (id,cancellationToken) =>
                {
                    return await _customValidatorsService.DoesDepartmentExist(id,cancellationToken);                    
                })
                .WithErrorCode($"{GuideErrorCodes.InvalidDepartment}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.DepartmentInvalid);

            RuleFor(u =>u.ManagerId)
                .MustAsync( async (id,cancellationToken) =>
                {
                    if (!id.HasValue) return true;
                    return await _customValidatorsService.DoesManagerExist(id.Value,cancellationToken);                    
                })
                .WithErrorCode($"{GuideErrorCodes.InvalidManager}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.ManagerInvalid);


        }
    }
}
