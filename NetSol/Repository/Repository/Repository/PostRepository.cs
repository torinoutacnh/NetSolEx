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
    [DIRegiter(typeof(IRepository<Post>), typeof(PostRepository), RegisterType.Transient)]
    public class PostRepository : Repository<Post>
    {
        public PostRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
