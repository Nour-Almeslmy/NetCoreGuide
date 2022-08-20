using CoreGuide.Common.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.SignIn.Output
{
    public class SignInOutput : BaseOutput
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
