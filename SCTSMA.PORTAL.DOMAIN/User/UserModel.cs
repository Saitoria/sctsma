using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTSMA.PORTAL.DOMAIN.User
{
    public class UserModel
    {
        public int? Userid { get; set; }

        public string? Username { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Phonenumber { get; set; }

        public string? Password { get; set; }
    }
}
