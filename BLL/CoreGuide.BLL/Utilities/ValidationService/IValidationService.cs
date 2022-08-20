using CoreGuide.BLL.Models.FluentValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.ValidationService
{
    internal interface IValidationService
    {
        Task<FluentValidationResult> ValidateInputIfNullOrInvalidAsync<T>(T input);
    }
}
