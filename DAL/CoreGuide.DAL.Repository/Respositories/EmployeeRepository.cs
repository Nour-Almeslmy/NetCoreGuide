using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.DAL.Context;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.Respositories
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public EmployeeRepository(
            GuideContext context,
            ApplicationUserManager applicationUserManager
            ) : base(context)
        {
            _applicationUserManager = applicationUserManager;
        }

        public async Task<bool> CheckPasswordAsync(Employee user, string password)
        {
            if (user != null)
                return await _applicationUserManager.CheckPasswordAsync(user, password);
            return false;
        }
        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            var user = await _applicationUserManager.FindByNameAsync(userName);
            if (user != null)
                return await _applicationUserManager.CheckPasswordAsync(user, password);
            return false;
        }

        public async Task<IdentityResult> CreateUserAsync(Employee userEntity, string password = null)
        {
            return await _applicationUserManager.CreateAsync(userEntity, password); ;
        }

        public async Task<IdentityResult> CreateUserWithRoleAsync(Employee userEntity, string role, string password)
        {
            IdentityResult result = await _applicationUserManager.CreateAsync(userEntity, password);
            if (result.Succeeded)
                return await _applicationUserManager.AddToRoleAsync(userEntity, role);
            return result;
        }

        //public async Task<IdentityResult> UpdateUserRefreshTokenAsync(Guid userId, string refreshToken)
        //{
        //    var userEntity = await _applicationUserManager.FindByIdAsync(userId.ToString());
        //    if (userEntity == null)
        //    {
        //        return IdentityResult.Failed(new IdentityError() { Description = "Account Not Found." });
        //    }
        //    userEntity.RefreshToken = refreshToken;
        //    return await _applicationUserManager.UpdateAsync(userEntity);
        //}

        public async Task<IdentityResult> ChangePaswordAsync(Employee user, string oldPassword, string newPassword)
        {
            return await _applicationUserManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public Task<Employee> GetUserByIdWithRoleAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _applicationUserManager.Users
                                  .Include(user => user.UserRoles)
                                  .ThenInclude(userRole => userRole.Role)
                                  .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }
        public Task<Employee> GetUserByUserNameWithRoleAsync(string userName, CancellationToken cancellationToken)
        {
            return _applicationUserManager.Users
                                  .Include(user => user.UserRoles)
                                  .ThenInclude(userRole => userRole.Role)
                                  .SingleOrDefaultAsync(user => user.UserName == userName, cancellationToken);
        }
        public Task<Employee> GetUserByIdWithClaimsAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _applicationUserManager.Users
                                  .Include(user => user.UserClaims)
                                  .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }

        public Task<string> GeneratePasswordResetTokenAsync(Employee user)
        {
            return _applicationUserManager.GeneratePasswordResetTokenAsync(user);
        }
        public Task<bool> ValidateResetPasswordTokenAsync(Employee user, string token)
        {
            return _applicationUserManager.VerifyUserTokenAsync(user, _applicationUserManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        }

        public Task<IdentityResult> ResetPasswordAsync(Employee user, string token, string newPassword)
        {
            if (user != null)
            {
                return _applicationUserManager.ResetPasswordAsync(user, token, newPassword);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError() { Description = "User Not Found" }));
        }

        public Task<IdentityResult> DeleteUserAsync(Employee userEntity)
        {
            if (userEntity != null)
            {
                return _applicationUserManager.DeleteAsync(userEntity);
            }
            return Task.FromResult(IdentityResult.Failed(new IdentityError() { Description = "User Not Found" }));
        }

        public Task<bool> DoesEmployeeExistAsync(string userName, CancellationToken cancellationToken)
        {
            return _applicationUserManager.Users.AsNoTracking().AnyAsync(e => e.UserName == userName, cancellationToken);
        }
        public Task<bool> DoesEmployeeExistAsync(Guid id, CancellationToken cancellationToken)
        {
            return _applicationUserManager.Users.AsNoTracking().AnyAsync(e => e.Id == id, cancellationToken);
        }

        public Task<Employee> GetByUserNameAsync(string userName)
        {
            return _applicationUserManager.FindByNameAsync(userName);
        }

        public async Task<bool> IsUserLockedoutAsync(Employee employee)
        {
            return await _applicationUserManager.IsLockedOutAsync(employee);
        }
        public async Task<IdentityResult> AddFailedAccessAsync(Employee employee)
        {
            return await _applicationUserManager.AccessFailedAsync(employee);
        }
        public async Task<IdentityResult> ResetFailedAccessAsync(Employee employee)
        {
            return await _applicationUserManager.ResetAccessFailedCountAsync(employee);
        }

    }
}
