using CoreGuide.BLL.Models.Enums;
using CoreGuide.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.TokenService
{
    internal interface ITokenService
    {
        string GenerateAccessToken(Employee employee);
        Task<(RefreshToken refreshToken, TokenValidationOutput? validationOutput)> GenerateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken, Guid? employeeId = null);
    }
}
