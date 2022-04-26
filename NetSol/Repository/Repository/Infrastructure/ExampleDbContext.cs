using Core.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository.Contract.Infrastructure;
using System;
using System.IO;
using System.Reflection;

namespace Repository.Infrastructure
{
    public sealed partial class ExampleDbContext : BaseDbContext
    {
        public readonly int CommandTimeoutInSecond = 20 * 60;

        public ExampleDbContext()
        {
            Database.SetCommandTimeout(CommandTimeoutInSecond);
        }

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(CommandTimeoutInSecond);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = SystemHelper.ConnectionString ?? configuration.GetConnectionString("Developement");

                optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(
                        typeof(ExampleDbContext).GetTypeInfo().Assembly.GetName().Name);

                    sqlServerOptionsAction.MigrationsHistoryTable("Migration");
                });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
