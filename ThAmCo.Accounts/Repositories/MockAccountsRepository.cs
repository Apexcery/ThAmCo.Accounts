using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ThAmCo.Accounts.Data.Account;
using ThAmCo.Accounts.Enums;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.User;
using ThAmCo.Accounts.Responses;

namespace ThAmCo.Accounts.Repositories
{
    public class MockAccountsRepository : IAccountsRepository
    {
        private readonly IMapper _mapper;
        private static readonly List<Account> StoredAccounts = new List<Account>();

        public MockAccountsRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<BaseResponse<Account>> GetUserAccountById(Guid accountId)
        {
            if (StoredAccounts.All(x => x.Id != accountId.ToString()))
                return null;

            return new OkResponse<Account>(StoredAccounts.Single(x => x.Id == accountId.ToString()));
        }

        public async Task<BaseResponse<Account>> GetUserAccountByUsername(string username)
        {
            if (StoredAccounts.All(x => !x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)))
                return null;

            return new OkResponse<Account>(StoredAccounts.Single(x => x.Username.Equals(username, StringComparison.CurrentCultureIgnoreCase)));
        }

        public async Task<BaseResponse<List<Account>>> GetUserAccounts(int limit)
        {
            return new OkResponse<List<Account>>(StoredAccounts.Take(limit).ToList());
        }

        public async Task<BaseResponse<AppUser>> AddAccountRole(Guid accountId, AccountRoleEnum role)
        {
            if (StoredAccounts.All(x => x.Id != accountId.ToString()))
                return new ErrorResponse<AppUser>("No account could be found with the specified ID.") { StatusCode = StatusCodes.Status404NotFound };

            var account = StoredAccounts.Single(x => x.Id == accountId.ToString());

            if (account.Roles.Any(currentRole => currentRole.Equals(role.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                return new ErrorResponse<AppUser>("The specified account already has the specified role.") { StatusCode = StatusCodes.Status409Conflict };

            account.Roles.Add(role.ToString());
            return new OkResponse<AppUser>(_mapper.Map<AppUser>(account));
        }

        public async Task<BaseResponse<AppUser>> RemoveAccountRole(Guid accountId, AccountRoleEnum role)
        {
            if (StoredAccounts.All(x => x.Id != accountId.ToString()))
                return new ErrorResponse<AppUser>("No account could be found with the specified ID.") { StatusCode = StatusCodes.Status404NotFound };

            var account = StoredAccounts.Single(x => x.Id == accountId.ToString());

            if (!account.Roles.Any(currentRole => currentRole.Equals(role.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                return new ErrorResponse<AppUser>("The specified account does not have the specified role.") { StatusCode = StatusCodes.Status404NotFound };

            account.Roles.Remove(role.ToString());
            return new OkResponse<AppUser>(_mapper.Map<AppUser>(account));
        }
    }
}
