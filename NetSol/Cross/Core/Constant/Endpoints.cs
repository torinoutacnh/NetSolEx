using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constant
{
    public static class Endpoints
    {
        public static class UserEndpoint
        {
            public const string Area = "User/";
            public const string BaseEndpoint = "~/" + Area + "get-user/";
            public const string PostEndpoint = "~/" + Area + "get-post/";
        }
        public static class AuthEndpoint
        {
            public const string Area = "Auth/";
            public const string BaseEndpoint = "~/" + Area + "get-token/";
            public const string PostEndpoint = "~/" + Area + "reset-token/";
        }
    }
}
