using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extension
{
    public enum RegisterType
    {
        Transient=0,
        Scope=1,
        Singleton=2,
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DIRegiterAttribute : Attribute
    {
        public Type T { get; set; }
        public Type I { get; set; }
        public RegisterType Register { get; set; }

        public DIRegiterAttribute(Type T,Type I, RegisterType registerType)
        {
            this.T = T;
            this.I = I;
            Register = registerType;
        }
    }
}
