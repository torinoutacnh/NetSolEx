using Repository.Contract.Infrastructure;
using Repository.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
