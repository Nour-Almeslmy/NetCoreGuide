using CoreGuide.BLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.FluentValidationResult
{
    public class FluentValidationResult
    {
        public bool IsValid { get; set; }
        public GuideErrorCodes ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
