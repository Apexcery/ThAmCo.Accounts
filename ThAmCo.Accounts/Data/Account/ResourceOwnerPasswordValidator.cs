using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace ThAmCo.Accounts.Data.Account
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<AppUser> _userManager;

        public ResourceOwnerPasswordValidator(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userManager.FindByNameAsync(context.UserName).Result;
            context.Result = new GrantValidationResult(user.Id.ToString(), "password", null, "local", null);
            return Task.FromResult(context.Result);
        }
    }
}
