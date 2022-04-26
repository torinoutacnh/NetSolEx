using Core.Constant;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace NetSol.Utils
{
    public static class ServiceExt
    {
        public static IServiceCollection AddSystemSetting(this IServiceCollection services, SystemHelperModel systemSettingModel)
        {
            SystemHelperModel.Instance = systemSettingModel ?? new SystemHelperModel();

            return services;
        }
    }
}
