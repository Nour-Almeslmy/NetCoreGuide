using CoreGuide.BLL.Models.Accounts.RegisterUser.Input;
using CoreGuide.BLL.Models.Accounts.SignIn.Input;
using CoreGuide.BLL.Models.Accounts.SignIn.Output;
using CoreGuide.Common.Entities.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Manager.Accounts
{
    public interface IAccountsManager
    {
        Task<SignInOutput> RefreshToken(string refreshToken, CancellationToken cancellationToken);
        Task<BaseOutput> RegisterEmployee(RegisterUserInput input);
        Task<SignInOutput> SignIn(SignInInput input, CancellationToken cancellationToken);
    }
}
