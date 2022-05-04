using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Contract.Infrastructure
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        protected BaseDbContext()
        {
            Database.Migrate();
        }

        protected BaseDbContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();
        }

        Task<EntityEntry> IDbContext.AddAsync(object entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<EntityEntry<TEntity>> IDbContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        Task<object> IDbContext.FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IDbContext.FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<object> IDbContext.FindAsync(Type entityType, params object[] keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
