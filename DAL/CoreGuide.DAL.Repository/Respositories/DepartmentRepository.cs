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
    internal class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(GuideContext context) : base(context)
        {
        }

        public Task<bool> DoesDepartmentExistAsync(int id, CancellationToken cancellationToken)
        {
            return AnyAsync(d => d.Id == id, cancellationToken);
        }

    }
}
