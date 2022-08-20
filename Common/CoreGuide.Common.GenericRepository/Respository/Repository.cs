using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreGuide.Common.GenericRepository.Respository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected DbSet<TEntity> DbSet { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        #region Sync
        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public List<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public TEntity FindItem(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToList();
        }

        public List<TEntity> GetByPage(int pageNumber, int pageSize)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return DbSet.Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return DbSet.Where(predicate).ToList().Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToList();
            return DbSet.Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToList();
            return DbSet.Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToList();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).Count();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate)
        {
            return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
        }
        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate)
        {
            return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).Any();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void RemoveAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> list = DbSet.Where(predicate).ToList();

            if (list != null && list.Count > 0)
                DbSet.RemoveRange(list);
        }

        public List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToList();
            return DbSet.Where(predicate).OrderBy(orderPredicate).Take(topCount).ToList();
        }
        public List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToList();
            return DbSet.Where(predicate).OrderBy(orderPredicate).Take(topCount).ToList();
        }

        public TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
            return DbSet.Where(predicate).OrderBy(orderPredicate).FirstOrDefault();
        }

        public TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
            return DbSet.Where(predicate).OrderBy(orderPredicate).FirstOrDefault();
        }
        public void Deatach(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Detached;
        }
        #endregion

        #region Async
        public ValueTask<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(new object[] { id }, cancellationToken);
        }
        public ValueTask<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return DbSet.ToListAsync(cancellationToken);
        }

        public Task<TEntity> FindItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetByPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return DbSet.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindByPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return DbSet.Where(predicate).Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindByPageAndOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, CancellationToken cancellationToken, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindByPageAndOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize, CancellationToken cancellationToken, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToListAsync(cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).CountAsync(cancellationToken);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefaultAsync(cancellationToken);
        }
        public Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return DbSet.Where(predicate).AnyAsync(cancellationToken);
        }

        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return DbSet.AddAsync(entity, cancellationToken);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            return DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return DbSet.CountAsync(cancellationToken);
        }

        public Task<List<TEntity>> FindTopListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, CancellationToken cancellationToken, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToListAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).Take(topCount).ToListAsync(cancellationToken);
        }
        public Task<List<TEntity>> FindTopListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, CancellationToken cancellationToken, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToListAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).Take(topCount).ToListAsync(cancellationToken);
        }

        public Task<TEntity> FindItemByOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, CancellationToken cancellationToken, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefaultAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<TEntity> FindItemByOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, CancellationToken cancellationToken, bool orderDesc = false)
        {
            if (orderDesc)
                return DbSet.Where(predicate).OrderByDescending(orderPredicate).FirstOrDefaultAsync(cancellationToken);
            return DbSet.Where(predicate).OrderBy(orderPredicate).FirstOrDefaultAsync(cancellationToken);
        }
        #endregion
    }
}
