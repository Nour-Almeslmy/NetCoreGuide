using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.SignIn.Input
{
    public record SignInInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
