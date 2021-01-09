using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Enums;
using ThAmCo.Accounts.Models.User;
using ThAmCo.Accounts.Responses;

namespace ThAmCo.Accounts.Interfaces
{
    public interface IAccountsRepository
    {
        public Task<BaseResponse<Account>> GetUserAccountById(Guid accountId);
        public Task<BaseResponse<Account>> GetUserAccountByUsername(string username);
        public Task<BaseResponse<List<Account>>> GetUserAccounts(int limit);
        public Task<BaseResponse<AppUser>> AddAccountRole(Guid accountId, AccountRoleEnum role);
        public Task<BaseResponse<AppUser>> RemoveAccountRole(Guid accountId, AccountRoleEnum role);
    }
}
