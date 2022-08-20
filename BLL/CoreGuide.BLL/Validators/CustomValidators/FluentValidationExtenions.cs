using CoreGuide.BLL.Models.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators.CustomValidators
{
    internal static class FluentValidationExtenions
    {

        public static IRuleBuilderOptions<T, IFormFile> MaximumSize<T>(this IRuleBuilder<T, IFormFile> ruleBuilder, int sizeInMBs)
        {
            return ruleBuilder.SetValidator(new FileSizeValidator<T>(sizeInMBs)).WithErrorCode($"{GuideErrorCodes.InvalidFile}");
        }
        public static IRuleBuilderOptions<T, IFormFile> AllowedTypes<T>(this IRuleBuilder<T, IFormFile> ruleBuilder, string[] allowedFileExtensions)
        {
            return ruleBuilder.SetValidator(new FileContentTypeValidator<T>(allowedFileExtensions)).WithErrorCode($"{GuideErrorCodes.InvalidFile}");
        }
    }
}
