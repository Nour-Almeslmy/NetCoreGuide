using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.Entities.Output
{
    public class BaseOutput
    {
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorMessage { get; set; }
        public string InternalError { get; set; }
        public string InternalErrorDescription { get; set; }
    }
}
