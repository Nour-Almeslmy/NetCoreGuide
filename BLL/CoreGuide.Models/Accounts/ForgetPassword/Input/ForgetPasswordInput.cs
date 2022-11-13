using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.ForgetPassword.Input
{
    public record ForgetPasswordInput
    {
        public string UserName { get; set; }
    }
}
