using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThAmCo.Accounts.Models.Auth
{
    public class AuthDto
    {
        public UserLoginDto LoginModel { get; set; }
        public UserRegisterDto RegisterModel { get; set; }
    }
}
