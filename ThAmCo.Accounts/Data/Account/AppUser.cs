using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ThAmCo.Accounts.Data.Account
{
    public class AppUser : IdentityUser
    {
        public string Forename { get; set; }
        public string Surname { get; set; }

        public string Fullname => Forename + " " + Surname;
    }
}
