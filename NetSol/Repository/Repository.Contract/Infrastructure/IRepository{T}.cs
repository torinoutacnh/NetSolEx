using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Infrastructure
{
    public interface IRepository<T> : IDisposable where T : Entity, new()
    {
        IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties);
        T Add(T entity);
        List<T> AddRange(params T[] entities);
        void Update(T entity, params Expression<Func<T, object>>[] changedProperties);
        void Update(T entity, params string[] changedProperties);
        void Update(T entity);
        void Delete(T entity, bool isPhysicalDelete = false);
    }
}
