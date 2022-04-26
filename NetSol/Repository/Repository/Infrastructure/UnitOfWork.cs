using Microsoft.Extensions.DependencyInjection;
using Repository.Contract.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private bool disposed = false;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _dbContext = serviceProvider.GetRequiredService<IDbContext>();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
