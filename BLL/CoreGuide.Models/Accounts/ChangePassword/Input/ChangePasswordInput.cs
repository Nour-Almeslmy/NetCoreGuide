using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.Accounts.ChangePassword.Input
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
