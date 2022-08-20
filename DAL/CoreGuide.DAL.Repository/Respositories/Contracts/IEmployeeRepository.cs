using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.DAL.Context.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.Respositories.Contracts
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IdentityResult> ChangePaswordAsync(Employee user, string oldPassword, string newPassword);
        Task<bool> CheckPasswordAsync(Employee user, string password);
        Task<IdentityResult> CreateUserAsync(Employee userEntity, string password = null);
        Task<IdentityResult> CreateUserWithRoleAsync(Employee userEntity, string role, string password);
        Task<IdentityResult> DeleteUserAsync(Employee userEntity);
        Task<string> GeneratePasswordResetTokenAsync(Employee user);
        Task<Employee> GetUserByIdWithRoleAsync(Guid userId, CancellationToken cancellationToken);
        Task<Employee> GetUserByIdWithClaimsAsync(Guid userId, CancellationToken cancellationToken);
        Task<IdentityResult> ResetPasswordAsync(Employee user, string token, string newPassword);
        Task<bool> ValidateResetPasswordTokenAsync(Employee user, string token);
        Task<bool> DoesEmployeeExistAsync(string userName, CancellationToken cancellationToken);
        Task<bool> DoesEmployeeExistAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<Employee> GetByUserNameAsync(string userName);
        Task<bool> IsUserLockedoutAsync(Employee employee);
        Task<IdentityResult> AddFailedAccessAsync(Employee employee);
        Task<IdentityResult> ResetFailedAccessAsync(Employee employee);
        Task<Employee> GetUserByUserNameWithRoleAsync(string userName, CancellationToken cancellationToken);
    }
}
