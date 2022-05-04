using Core.Constant;
using Core.Extension;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Contract.Infrastructure;
using System;
using System.Linq;
using System.Reflection;

namespace NetSol.Utils
{
    public static class ServiceExt
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            var assemblyTypes = Assembly.GetAssembly(typeof(Repository<>)).GetTypes().Where(x => !x.IsInterface && !x.IsAbstract && x.GetInterfaces().Any(i => i.IsGenericType && typeof(IRepository<>).IsAssignableFrom(i.GetGenericTypeDefinition())));

            foreach (var genericType in assemblyTypes)
            {
                services.AddTransient(genericType.GetInterfaces().First(), genericType);
            }
            return services;
        }
    }
}
