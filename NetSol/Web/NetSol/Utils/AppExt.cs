using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetSol.Utils
{
    public static class AppExt
    {
        public static IApplicationBuilder UseSystemSetting(this IApplicationBuilder app)
        {
            SystemHelperModel.Configs = app.ApplicationServices.GetService<IConfiguration>();

            return app;
        }
    }
}
