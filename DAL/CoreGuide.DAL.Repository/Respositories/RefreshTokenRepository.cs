using CoreGuide.Common.GenericRepository.Respository;
using CoreGuide.DAL.Context;
using CoreGuide.DAL.Context.Entities;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.Respositories
{
    internal class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(GuideContext context) : base(context)
        {
        }

        public Task<RefreshToken> GetWithTokenAsync(string token, CancellationToken cancellationToken)
        {
            return LastOrDefaultAsync(r => r.Token == token, r => r.Created,cancellationToken);
        }

        public Task<List<RefreshToken>> GetUserActiveRefreshTokensAsync(Guid employeeId,CancellationToken cancellationToken)
        {
            return FindListAsync(r => r.EmployeeId == employeeId && DateTime.UtcNow < r.Expires && r.Revoked == null,cancellationToken);
        }

       
        public Task<RefreshToken> GetLastRefreshTokenAsync(Guid employeeID, CancellationToken cancellationToken)
        {
            return LastOrDefaultAsync(r => r.EmployeeId == employeeID, r => r.Created,cancellationToken);
        }
    }
}
