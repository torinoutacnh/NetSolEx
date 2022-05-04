﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.JWT.JWTModel
{
    public class JWT
    {
        public static JWT Setting { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecrectKey { get; set; }
    }
}
