using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.BLL.Models.Accounts.ConfirmOTP.Input;
using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators
{
    internal class ConfirmOTPInputValidator : AbstractValidator<ConfirmOTPInput>
    {
        private readonly ICustomValidatorsService _customValidatorsService;

        public ConfirmOTPInputValidator(ICustomValidatorsService customValidatorsService)
        {
            CascadeMode = CascadeMode.Stop;
            _customValidatorsService = customValidatorsService;
            RuleFor(u => u.UserName)
               .NotEmpty()
               .WithErrorCode($"{GuideErrorCodes.MissingValue}")
               .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.UserNameNull)
               .MustAsync(async (id, cancellationToken) =>
               {
                   return await _customValidatorsService.DoesEmployeeExist(id,cancellationToken);
               })
               .WithErrorCode($"{GuideErrorCodes.UserNotFound}")
               .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.UserNotFound);
            RuleFor(o => o.Code)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.OTPNull);
            RuleFor(u => u.NewPassword)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NewPasswordNull)
                .Must(_customValidatorsService.ValidatePassword)
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.NewPasswordInvalid);
            RuleFor(u => u.ConfirmedPassword)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.ConfirmedPasswordNull)
                .Equal(c => c.NewPassword)
                .WithErrorCode($"{GuideErrorCodes.InvalidPassowrd}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.ConfirmedPasswordInvalid);
        }
    }
}
