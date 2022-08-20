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
    public interface IRepository<TEntity> : ICovariantRepository<TEntity>
        where TEntity : class
    {
        #region Sync
        TEntity GetById(int id);
        List<TEntity> GetAll();
        List<TEntity> GetByPage(int pageNumber, int pageSize);
        TEntity FindItem(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindByPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false);
        List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false);
        int Count(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        int Count();
        List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, bool orderDesc = false);
        List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, bool orderDesc = false);
        TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, bool orderDesc = false);
        TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, bool orderDesc = false);
        void RemoveAllBy(Expression<Func<TEntity, bool>> predicate);
        void Deatach(TEntity entity);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate);
        #endregion

        #region Async
        ValueTask<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
        ValueTask<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<TEntity>> GetByPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<TEntity> FindItemAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<List<TEntity>> FindByPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<List<TEntity>> FindByPageAndOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, CancellationToken cancellationToken, bool orderDesc = false);
        Task<List<TEntity>> FindByPageAndOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize,CancellationToken cancellationToken, bool orderDesc = false);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,CancellationToken cancellationToken);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate,CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate,CancellationToken cancellationToken);
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity,CancellationToken cancellationToken);
        Task AddRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<List<TEntity>> FindTopListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, CancellationToken cancellationToken, bool orderDesc = false);
        Task<List<TEntity>> FindTopListAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, CancellationToken cancellationToken, bool orderDesc = false);
        Task<TEntity> FindItemByOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, CancellationToken cancellationToken, bool orderDesc = false);
        Task<TEntity> FindItemByOrderAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, CancellationToken cancellationToken, bool orderDesc = false);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, CancellationToken cancellationToken);
        #endregion
    }
    // TODO: check covariance
    public interface ICovariantRepository<out TEntity> where TEntity : class
    {
        //IEnumerable<TEntity> GetAllNew();
    }
}
