using CoreGuide.DAL.Context;
using CoreGuide.DAL.Repository.Respositories.Contracts;
using CoreGuide.DAL.Repository.UnitOfWork.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.DAL.Repository.UnitOfWork
{
    internal class GuideUnitOfWork : Common.GenericRepository.UnitOfWork.UnitOfWork, IGuideUnitOfWork
    {

        public GuideUnitOfWork(
            GuideContext dbContext
            ) : base(dbContext)
        {
        }

    }
}
