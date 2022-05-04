using Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constant
{
    public static class SystemHelper
    {
        public static SystemHelperModel Setting => SystemHelperModel.Instance;
        public static JWT.JWTModel.JWT JWTSetting => JWT.JWTModel.JWT.Setting;
        public static IConfiguration Configs => SystemHelperModel.Configs;
        public static string ConnectionString => SystemHelperModel.Configs?.GetConnectionString(IsProduction());

        public static string IsProduction()
        {
            var env = SystemHelperModel.Configs.AsEnumerable().Where(c => c.Key.Equals("ASPNETCORE_ENVIRONMENT")).FirstOrDefault();
            if(!env.Equals(new KeyValuePair<string,string>()))
            {
                return env.Equals("Developement") ? "Developement" : "Production";
            }
            return "Developement";
        }
    }
}
