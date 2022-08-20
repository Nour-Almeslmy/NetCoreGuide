using CoreGuide.Common.GenericRepository.Respository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.GenericRepository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public int Submit()
        {
            return _context.SaveChanges();
        }
        public Task<int> SubmitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
