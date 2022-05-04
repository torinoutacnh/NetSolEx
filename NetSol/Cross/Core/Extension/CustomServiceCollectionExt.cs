using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extension
{
    public static class CustomServiceCollectionExt
    {
        public static IServiceCollection AddServiceFromAttribute(this IServiceCollection services)
        {
            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        from type in assembly.GetTypes()
                        where type.IsDefined(typeof(DIRegiterAttribute))
                        select type;
            foreach (var type in types)
            {
                var register = (DIRegiterAttribute)Attribute.GetCustomAttribute(type, typeof(DIRegiterAttribute));
                switch (register.Register)
                {
                    case RegisterType.Transient:
                        {
                            services.AddTransient(register.T, register.I);
                            break;
                        }
                    case RegisterType.Scope:
                        {
                            services.AddScoped(register.T, register.I);
                            break;
                        }
                    case RegisterType.Singleton:
                        {
                            services.AddSingleton(register.T, register.I);
                            break;
                        }
                    default: break;
                }

            }
            return services;
        }
    }
}
