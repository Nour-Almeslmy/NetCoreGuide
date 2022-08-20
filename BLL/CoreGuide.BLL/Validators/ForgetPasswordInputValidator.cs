using CoreGuide.BLL.Business.Utilities;
using CoreGuide.BLL.Business.Validators.CustomValidators;
using CoreGuide.BLL.Models.Accounts.ForgetPassword.Input;
using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators
{
    internal class ForgetPasswordInputValidator : AbstractValidator<ForgetPasswordInput>
    {
        private readonly ICustomValidatorsService _customValidatorsService;

        public ForgetPasswordInputValidator(ICustomValidatorsService customValidatorsService)
        {

            _customValidatorsService = customValidatorsService;
            CascadeMode = CascadeMode.Stop;
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
        }
    }
}
