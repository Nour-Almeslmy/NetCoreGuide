using CoreGuide.BLL.Business.Utilities;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Validators.CustomValidators
{
    internal class FileSizeValidator<T> : PropertyValidator<T, IFormFile>
    {
        private readonly double _maximunAllowedSizeInMBs;
        private IFormFile _file;
        public FileSizeValidator(double maximunAllowedSizeInMBs)
        {
            _maximunAllowedSizeInMBs = maximunAllowedSizeInMBs;
        }
        public override string Name => "File size validator";

        public override bool IsValid(ValidationContext<T> context, IFormFile value)
        {
            if (value == null)
                return false;
            return value?.Length <= _maximunAllowedSizeInMBs * 1024 * 1024;

        }
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return string.Format(BusinessStrings.Resources.ErrorMessagesKeys.FileSizeInvalid, _maximunAllowedSizeInMBs);
        }
    }
}
