using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SystemHelperModel
    {
        public static SystemHelperModel Instance { get; set; }
        public static IConfiguration Configs { get; set; }
        public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name;
        public string Version { get; set; }
        public string Domain { get; set; } = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        public string Host { get; set; } = System.Net.Dns.GetHostName();
    }
}
