using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.JWT.LoadSetting
{
    public static class LoadJWTSetting
    {
        public static IServiceCollection AddJWTSetting(this IServiceCollection services, JWTModel.JWT model)
        {
            JWTModel.JWT.Setting = model ?? new JWTModel.JWT();
            return services;
        }
    }
}
