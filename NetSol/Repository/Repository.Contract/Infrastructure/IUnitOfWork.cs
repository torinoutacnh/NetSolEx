using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contract.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChange();
        Task<int> SaveChangeAsync();
    }
}
