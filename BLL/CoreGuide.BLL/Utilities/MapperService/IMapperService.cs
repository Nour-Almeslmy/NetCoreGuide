using CoreGuide.BLL.Models.Accounts.ForgetPassword.Input;
using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.BLL.Models.Department.Input;
using CoreGuide.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.MapperService
{
    internal interface IMapperService
    {
        Employee Map(RegisterUserInput input);
        Task<OTP> Map(ForgetPasswordInput input);
        Department Map(AddDepratmentInput input);
    }
}
