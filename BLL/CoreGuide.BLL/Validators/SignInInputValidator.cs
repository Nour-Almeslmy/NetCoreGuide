using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.BLL.Models.Accounts.SignIn.Input;
using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators
{
    internal class SignInInputValidator : AbstractValidator<SignInInput>
    {
        private readonly ICustomValidatorsService _customValidatorsService;

        public SignInInputValidator(ICustomValidatorsService customValidatorsService)
        {
            CascadeMode = CascadeMode.Stop;
            _customValidatorsService = customValidatorsService;

            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.UserNameNull)
                .MustAsync(async (userName, cancellationToken) =>
                {
                    return await _customValidatorsService.DoesEmployeeExist(userName, cancellationToken);
                })
                .WithErrorCode($"{GuideErrorCodes.UserNotFound}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.UserNotFound);

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithErrorCode($"{GuideErrorCodes.MissingValue}")
                .WithMessage(BusinessStrings.Resources.ErrorMessagesKeys.PasswordNull);
        }
    }
}
