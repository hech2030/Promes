using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTeam.Common.Requests.Auth
{
    public class RegisterRequest
    {
        public string userName { get; set; }
        public string fullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string RoleLabel { get; set; }
    }
}
