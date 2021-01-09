using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Accounts.Data;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Enums;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.User;
using ThAmCo.Accounts.Responses;

namespace ThAmCo.Accounts.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AccountDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsRepository(AccountDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<BaseResponse<List<Account>>> GetUserAccounts(int limit)
        {
            var accounts = await _context.Users.OrderBy(x => x.UserName).Take(limit).ToListAsync();
            var accountsList = accounts.Select(account => AddRolesToAccount(account).Result).ToList();

            return new OkResponse<List<Account>>(accountsList);
        }

        private async Task<Account> AddRolesToAccount(AppUser account)
        {
            var mappedAccount = _mapper.Map<Account>(account);
            mappedAccount.Roles = await _userManager.GetRolesAsync(account);
            return mappedAccount;
        }

        public async Task<BaseResponse<Account>> GetUserAccountById(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Account>> GetUserAccountByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<AppUser>> AddAccountRole(Guid accountId, AccountRoleEnum role)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<AppUser>> RemoveAccountRole(Guid accountId, AccountRoleEnum role)
        {
            throw new NotImplementedException();
        }
    }
}
