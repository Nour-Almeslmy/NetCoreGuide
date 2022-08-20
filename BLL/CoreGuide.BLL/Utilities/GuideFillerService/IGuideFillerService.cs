using CoreGuide.BLL.Models.Enums;
using CoreGuide.BLL.Models.FileUploadResult;
using CoreGuide.BLL.Models.FluentValidationResult;
using CoreGuide.Common.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.GuideFillerService
{
    public interface IGuideFillerService
    {
        T FillOutput<T>(FluentValidationResult fluentValidationResult) where T : BaseOutput, new();
        T FillOutput<T>(GuideErrorCodes errorCode, string errorMessage) where T : BaseOutput, new();
        T FillOutput<T>(FileUploadResult uploadResult) where T : BaseOutput, new();
        T FillSuccessOutput<T>(string errorMessage = null) where T : BaseOutput, new();
        T FillSystemErrorOutput<T>() where T : BaseOutput, new();
    }
}
