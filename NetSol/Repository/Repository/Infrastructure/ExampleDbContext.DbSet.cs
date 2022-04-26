using Microsoft.EntityFrameworkCore;
using Repository.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Infrastructure
{
    public sealed partial class ExampleDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
