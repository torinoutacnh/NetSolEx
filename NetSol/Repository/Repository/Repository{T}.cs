using Microsoft.EntityFrameworkCore;
using Repository.Contract;
using Repository.Contract.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly IDbContext DbContext;

        private DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet != null)
                {
                    return _dbSet;
                }

                _dbSet = DbContext.Set<T>();
                return _dbSet;
            }
        }

        protected Repository(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected void TryAttach(T entity)
        {
            try
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
            }
            catch
            {
            }
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null, bool isIncludeDeleted = false, params Expression<Func<T, object>>[] includeProperties)
    {
            IQueryable<T> source = DbSet.AsNoTracking();
            if (predicate != null)
            {
                source = source.Where(predicate);
            }

            includeProperties = includeProperties?.Distinct().ToArray();
            if (includeProperties?.Any() ?? false)
            {
                Expression<Func<T, object>>[] array = includeProperties;
                foreach (Expression<Func<T, object>> navigationPropertyPath in array)
                {
                    source = source.Include(navigationPropertyPath);
                }
            }

            return isIncludeDeleted ? source.IgnoreQueryFilters() : source.Where((T x) => x.DeletedTime == null);
        }

        public T Add(T entity)
        {
            entity.DeletedTime = null;
            entity.UpdatedTime = entity.CreatedTime = DateTimeOffset.UtcNow;
            entity = DbSet.Add(entity).Entity;
            return entity;
        }

        public virtual List<T> AddRange(params T[] entities)
        {
            DateTimeOffset utcNow = DateTimeOffset.UtcNow;
            List<T> list = new List<T>();
            foreach (T val in entities)
            {
                val.CreatedTime = utcNow;
                T item = Add(val);
                list.Add(item);
            }

            return list;
        }

        public void Update(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            if (changedProperties?.Any() ?? false)
            {
                DbContext.Entry(entity).Property((T x) => x.UpdatedTime).IsModified = true;
                Expression<Func<T, object>>[] array = changedProperties;
                foreach (Expression<Func<T, object>> propertyExpression in array)
                {
                    DbContext.Entry(entity).Property(propertyExpression).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update(T entity, params string[] changedProperties)
        {
            TryAttach(entity);
            changedProperties = changedProperties?.Distinct().ToArray();
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            if (changedProperties?.Any() ?? false)
            {
                DbContext.Entry(entity).Property((T x) => x.UpdatedTime).IsModified = true;
                string[] array = changedProperties;
                foreach (string propertyName in array)
                {
                    DbContext.Entry(entity).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Update(T entity)
        {
            TryAttach(entity);
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity, bool isPhysicalDelete = false)
        {
            try
            {
                TryAttach(entity);
                if (!isPhysicalDelete)
                {
                    entity.DeletedTime = DateTimeOffset.UtcNow;
                    DbContext.Entry(entity).Property((T x) => x.DeletedTime).IsModified = true;
                }
                else
                {
                    DbSet.Remove(entity);
                }
            }
            catch (Exception)
            {
                RefreshEntity(entity);
                throw;
            }
        }

        public virtual void RefreshEntity(T entity)
        {
            DbContext.Entry(entity).Reload();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
