using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.BLL.Models.Accounts.ChangePassword.Input;
using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators
{
    internal class ChangePasswordInputValidator : AbstractValidator<ChangePasswordInput>
    {
        private readonly ICustomValidatorsService _customValidatorsService;

        public ChangePasswordInputValidator(ICustomValidatorsService customValidatorsService)
        {
            CascadeMode = CascadeMode.Stop;
            _customValidatorsService = customValidatorsService;
            RuleFor(c => c.CurrentPassword)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.CurrentPasswordNull)
                 .MustAsync(async (password, cancellationToken) =>
                 {
                     return await _customValidatorsService.IsCurrentPasswordCorrect(password);
                 })
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.CurrentPasswordWrong);
            RuleFor(c => c.NewPassword)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NewPasswordNull)
                .Must(_customValidatorsService.ValidatePassword)
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NewPasswordInvalid)
                .NotEqual(c => c.CurrentPassword)
                .WithErrorCode($"{GuideErrorCodes.NewPassowrdEqualsCurrent}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NewpasswordEqualsCurrent);
        }

    }
}
