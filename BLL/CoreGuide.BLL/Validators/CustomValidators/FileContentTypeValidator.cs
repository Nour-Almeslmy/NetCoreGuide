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
    internal class FileContentTypeValidator<T> : PropertyValidator<T, IFormFile>
    {
        private readonly string[] _allowedExtensions;
        private IFormFile _file;
        public FileContentTypeValidator(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        public override string Name => "File type validator";

        public override bool IsValid(ValidationContext<T> context, IFormFile value)
        {
            if (value == null)
                return false;
            _file = value;
            var extension = Path.GetExtension(_file.FileName).Remove(0, 1);
            return _allowedExtensions.Contains(extension.ToLower());

        }
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return string.Format(BusinessStrings.Resources.ErrorMessagesKeys.FileTypeInvalid, Path.GetExtension(_file?.FileName), string.Join(", ", _allowedExtensions));
        }
    }
}
