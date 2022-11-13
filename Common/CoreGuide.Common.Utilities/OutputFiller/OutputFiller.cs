using CoreGuide.Common.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.Utilities.OutputFiller
{
    internal class OutputFiller : IOutputFiller
    {
        public T FillOutput<T>(int errorCode, string errorMessage, string internalErrorDescription = null)
            where T : BaseOutput, new()
        {
            T output = new()
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                InternalErrorDescription = internalErrorDescription
            };

            return output;
        }


    }
}
