using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyTeam.Common.Requests.Auth
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
