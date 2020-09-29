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
        public string password { get; set; }
        public string emailAdress { get; set; }
        public int roleId { get; set; }
        public string role { get; set; }
    }
}
