using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Models.User;

namespace ThAmCo.Accounts.Mapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<AppUser, Account>().ReverseMap();
        }
    }
}
