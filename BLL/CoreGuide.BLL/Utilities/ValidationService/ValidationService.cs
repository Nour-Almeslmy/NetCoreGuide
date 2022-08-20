using CoreGuide.BLL.Models.Enums;
using CoreGuide.BLL.Models.FluentValidationResult;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.ValidationService
{
    internal class ValidationService : IValidationService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ValidationService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<FluentValidationResult> ValidateInputIfNullOrInvalidAsync<T>(T input)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<T>>();
            if (input == null)
            {
                return new FluentValidationResult()
                {
                    IsValid = false,
                    ErrorCode = GuideErrorCodes.NullReference,
                    ErrorMessage = BusinessStrings.Resources.ErrorMessagesKeys.InputNull
                };
            }

            var validationResult = await validator.ValidateAsync(input);
            if (!validationResult.IsValid)
            {
                var errorCode = Enum.Parse<GuideErrorCodes>(validationResult.Errors.FirstOrDefault().ErrorCode);
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                //_logger.Error(errorDetails.ToString());

                return new FluentValidationResult()
                {
                    IsValid = false,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage
                };
            }
            return new FluentValidationResult()
            {
                IsValid = true
            };
        }
    }
}
