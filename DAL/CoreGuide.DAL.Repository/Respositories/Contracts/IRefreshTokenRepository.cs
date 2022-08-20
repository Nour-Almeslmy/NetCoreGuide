using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.DAL.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.Respositories.Contracts
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken> GetLastRefreshTokenAsync(Guid employeeID, CancellationToken cancellationToken);
        Task<List<RefreshToken>> GetUserActiveRefreshTokensAsync(Guid employeeId, CancellationToken cancellationToken);
        Task<RefreshToken> GetWithTokenAsync(string token, CancellationToken cancellationToken);
    }
}
