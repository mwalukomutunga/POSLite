using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POSLite.Persistance
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<EntityEntry<TEntity>> DeleteAsync(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        ValueTask<TEntity> GetByID(object id);
        ValueTask<EntityEntry<TEntity>> Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}