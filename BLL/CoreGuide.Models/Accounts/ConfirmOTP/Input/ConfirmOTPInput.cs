using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.ConfirmOTP.Input
{
    public class ConfirmOTPInput
    {

        public string Code { get; set; }
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
