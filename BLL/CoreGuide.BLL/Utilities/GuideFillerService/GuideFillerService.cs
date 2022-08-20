using CoreGuide.BLL.Models.Enums;
using CoreGuide.BLL.Models.FileUploadResult;
using CoreGuide.BLL.Models.FluentValidationResult;
using CoreGuide.Common.Entities.Output;
using CoreGuide.Common.Utilities.OutputFiller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.GuideFillerService
{
    internal class GuideFillerService : IGuideFillerService
    {
        private readonly IOutputFiller _outputFiller;

        public GuideFillerService(IOutputFiller outputFiller)
        {
            _outputFiller = outputFiller;
        }

        public T FillOutput<T>(FluentValidationResult fluentValidationResult)
            where T : BaseOutput, new()
        {
            return _outputFiller.FillOutput<T>((int)fluentValidationResult.ErrorCode, fluentValidationResult.ErrorMessage);
        }
        public T FillOutput<T>(FileUploadResult uploadResult)
            where T : BaseOutput, new()
        {
            return _outputFiller.FillOutput<T>((int)GuideErrorCodes.ServerError, uploadResult.ErrorMessage);
        }

        public T FillOutput<T>(GuideErrorCodes errorCode, string errorMessage)
            where T : BaseOutput, new()
        {
            return _outputFiller.FillOutput<T>((int)errorCode, errorMessage);

        }

        public T FillSystemErrorOutput<T>()
            where T : BaseOutput, new()
        {
            var message = BusinessStrings.Resources.ErrorMessagesKeys.GeneralError;
            return _outputFiller.FillOutput<T>((int)GuideErrorCodes.ServerError, message);
        }
        public T FillSuccessOutput<T>(string errorMessage = null)
            where T : BaseOutput, new()
        {
            return _outputFiller.FillOutput<T>( (int)GuideErrorCodes.Success, errorMessage);
        }
    }
}
