using CoreGuide.BLL.Models.ConfigurationSettings;
using CoreGuide.BLL.Models.Enums;
using CoreGuide.Common.Entities.ConfigurationSettings;
using CoreGuide.Common.Utilities;
using CoreGuide.Common.Utilities.Utilities;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.TokenService
{
    internal class TokenService : ITokenService
    {
        private readonly IUtilities _utilities;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IGuideUnitOfWork _guideUnitOfWork;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly AccessTokenSettings _accessTokenSettings;

        public TokenService(
            IUtilities utilities,
            IRefreshTokenRepository refreshTokenRepository,
            IGuideUnitOfWork guideUnitOfWork,
            IOptionsSnapshot<RefreshTokenSettings> refreshTokenSettings,
            IOptionsSnapshot<AccessTokenSettings> accessTokenSettings
            )
        {
            _utilities = utilities;
            _refreshTokenRepository = refreshTokenRepository;
            _guideUnitOfWork = guideUnitOfWork;
            _refreshTokenSettings = refreshTokenSettings.Value;
            _accessTokenSettings = accessTokenSettings.Value;
        }

        #region Generate Access Token
        public string GenerateAccessToken(Employee employee)
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(Strings.JWTClaimNames.Id, employee.Id.ToString()),
                new Claim(Strings.JWTClaimNames.UserName, employee.UserName),
                new Claim(Strings.JWTClaimNames.Email, employee.Email),
                new Claim(Strings.JWTClaimNames.Role, employee.UserRoles.FirstOrDefault().Role.Name),
            });

            var signingCredentials = GetSigningCredetials(_accessTokenSettings.SecretKey);


            var securityTokenDescriptor = GetSecurityTokenDescriptor(claimsIdentity, signingCredentials,
                _accessTokenSettings.ExpirationTimeInMinutes, _accessTokenSettings.Issuer, _accessTokenSettings.Audience);

            var signedAndEncodedToken = ProduceToken(securityTokenDescriptor);

            return signedAndEncodedToken;
        }

        private SigningCredentials GetSigningCredetials(string secretKey)
        {
            SecurityKey securityKey = GetSecurityKey(secretKey);
            return new SigningCredentials(securityKey,
               SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
        }

        private SecurityKey GetSecurityKey(string secretKey)
        {
            byte[] SymmetricKey = Convert.FromBase64String(secretKey);
            return new SymmetricSecurityKey(SymmetricKey);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity claimsIdentity, SigningCredentials signingCredentials, int ExpirationTimeInMinutes, string issuer, string audience)
        {
            return new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Subject = claimsIdentity,
                SigningCredentials = signingCredentials,
                Expires = DateTime.Now.AddMinutes(ExpirationTimeInMinutes)
            };
        }

        private string ProduceToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(tokenDescriptor);
            var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
            return signedAndEncodedToken;
        }

        #endregion

        #region Generate refresh token
        public async Task<(RefreshToken refreshToken, TokenValidationOutput? validationOutput)> GenerateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken, Guid? employeeId = null)
        {
            var ipAddress = _utilities.GetUserIPAddress();
            RefreshToken oldRefreshToken = null;
            TokenValidationOutput? isValid = null;
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                oldRefreshToken = await _refreshTokenRepository.GetWithTokenAsync(refreshToken, cancellationToken);
                if (oldRefreshToken == null)
                    return (null, isValid);
                isValid = await IsValidRefreshTokenAsync(oldRefreshToken, cancellationToken);
                if (isValid != TokenValidationOutput.Valid)
                    return (null, isValid);
            }
            employeeId ??= oldRefreshToken.EmployeeId;
            var newRefreshToken = await GenerateRefreshToken(ipAddress, employeeId.Value, oldRefreshToken, cancellationToken);
            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            var result = await _guideUnitOfWork.SubmitAsync(cancellationToken);

            if (result > 0)
                return (newRefreshToken, isValid);

            return (null, null);
        }
        private async Task<RefreshToken> GenerateRefreshToken(string ipAddress, Guid employeeID, RefreshToken oldToken, CancellationToken cancellationToken)
        {
            var newRefreshToken = GenerateUniqueRandomToken();
            if (oldToken == null)
                await RevokeLastRefreshToken(employeeID, newRefreshToken, ipAddress, cancellationToken);
            else
                UpdateOldToken(oldToken, newRefreshToken, ipAddress);
            return new RefreshToken
            {
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddMinutes(_refreshTokenSettings.ExpirationTimeInMinutes),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
                EmployeeId = employeeID
            };
        }

        private string GenerateUniqueRandomToken()
        {
            var rnd = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rnd.GetBytes(randomBytes);
            var newGuid = Guid.NewGuid().ToByteArray();
            var tokenInBytes = newGuid.Concat(randomBytes).ToArray();
            return Convert.ToBase64String(tokenInBytes);
        }

        private async Task RevokeLastRefreshToken(Guid employeeID, string newRefreshToken, string ipAddress, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetLastRefreshTokenAsync(employeeID, cancellationToken);
            if (refreshToken != null)
                UpdateOldToken(refreshToken, newRefreshToken, ipAddress);
        }

        private void UpdateOldToken(RefreshToken oldToken, string newRefreshToken, string ipAddress)
        {
            oldToken.Revoked = DateTime.UtcNow;
            oldToken.RevokedByIp = ipAddress;
            oldToken.ReplacedByToken = newRefreshToken;
            _refreshTokenRepository.Update(oldToken);
        }
        #endregion

        #region IsValidRefreshToken
        private async Task<TokenValidationOutput> IsValidRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            if (!refreshToken.IsActive)
            {
                await RevokeUserRefreshTokens(refreshToken.EmployeeId, cancellationToken);
                return TokenValidationOutput.Invalid;
            }
            if (refreshToken.IsExpired)
            {
                return TokenValidationOutput.Expired;
            }
            return TokenValidationOutput.Valid;
        }

        private async Task RevokeUserRefreshTokens(Guid employeeId, CancellationToken cancellationToken)
        {
            var refreshTokens = await _refreshTokenRepository.GetUserActiveRefreshTokensAsync(employeeId, cancellationToken);
            foreach (var token in refreshTokens)
            {
                token.Revoked = DateTime.Now;
                token.RevokedByIp = _utilities.GetUserIPAddress();
            }
            await _guideUnitOfWork.SubmitAsync(cancellationToken);
        }

        #endregion

    }
}
