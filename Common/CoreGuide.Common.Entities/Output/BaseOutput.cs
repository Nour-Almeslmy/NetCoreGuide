using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.Entities.Output
{

    //public record BaseOutput(int Age,string ErrorDescription, string ErrorMessage, string InternalError, string InternalErrorDescription) { }

    public record BaseOutput
    {
        public int ErrorCode { get; init; }
        public string ErrorDescription { get; init; }
        public string ErrorMessage { get; init; }
        public string InternalError { get; init; }
        public string InternalErrorDescription { get; init; }
    }
}
