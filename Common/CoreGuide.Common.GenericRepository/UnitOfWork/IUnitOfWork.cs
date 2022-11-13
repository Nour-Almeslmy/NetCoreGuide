using CoreGuide.Common.GenericRepository.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.Common.GenericRepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        int Submit();
        Task<int> SubmitAsync(CancellationToken cancellationToken);
    }
}
