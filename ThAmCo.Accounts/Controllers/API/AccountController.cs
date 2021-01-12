using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThAmCo.Accounts.Enums;
using ThAmCo.Accounts.Interfaces;

namespace ThAmCo.Accounts.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountController(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = "thamco_api")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity;
            if (identity != null)
                return Ok(identity.GetSubjectId());

            return BadRequest("Couldn't retrieve account details.");
        }

        [HttpGet("accounts")]
        public async Task<ActionResult> GetMultipleAccounts(int limit = 10)
        {
            if (limit < 0)
                return BadRequest($"Specified limit must be above 0. (limit = {limit})");

            var accounts = await _accountsRepository.GetUserAccounts(limit);
            if (accounts?.Value == null)
                return BadRequest(new {Message = "Account list was invalid."});

            return Ok(accounts.Value);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetAccountById(string id)
        {
            var validId = Guid.TryParse(id, out var accountId);
            if (!validId)
                return BadRequest("You have entered an invalid account ID.");

            var account = await _accountsRepository.GetUserAccountById(accountId);
            if (account?.Value == null)
                return NotFound(new {AccountId = id});

            return Ok(account.Value);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult> GetAccountByUsername(string username)
        {
            var account = await _accountsRepository.GetUserAccountByUsername(username);
            if (account?.Value == null)
                return NotFound(new {AccountUsername = username});

            return Ok(account.Value);
        }

        [HttpPut("addRole/{id}/{role}")]
        public async Task<ActionResult> AddRoleToAccount(string id, string role)
        {
            var isIdValid = Guid.TryParse(id, out var accountId);
            var isRoleValid = Enum.TryParse<AccountRoleEnum>(role, true, out var roleToAdd);

            if (!isIdValid || !isRoleValid)
                return BadRequest("The id or role that you entered is not valid.");

            var response = await _accountsRepository.AddAccountRole(accountId, roleToAdd);

            if (response.IsError || response.Value == null)
                if (response.StatusCode == StatusCodes.Status404NotFound)
                    return NotFound(response.Message);
                else
                    return BadRequest(response.Message);

            return Ok(response.Value);
        }

        [HttpPut("removeRole/{id}/{role}")]
        public async Task<ActionResult> RemoveAccountRole(string id, string role)
        {
            var isIdValid = Guid.TryParse(id, out var accountId);
            var isRoleValid = Enum.TryParse<AccountRoleEnum>(role, true, out var roleToRemove);

            if (!isIdValid || !isRoleValid)
                return BadRequest("The id or role that you entered is not valid.");

            var response = await _accountsRepository.RemoveAccountRole(accountId, roleToRemove);

            if (response.IsError || response.Value == null)
                if (response.StatusCode == StatusCodes.Status404NotFound)
                    return NotFound(response.Message);
                else
                    return BadRequest(response.Message);

            return Ok(response.Value);
        }
    }
}
