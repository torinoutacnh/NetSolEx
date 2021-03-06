using Core.Extension;
using Repository.Contract.Infrastructure;
using Repository.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    [DIRegiter(typeof(IRepository<User>), typeof(UserRepository), RegisterType.Transient)]
    public class UserRepository : Repository<User>
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
