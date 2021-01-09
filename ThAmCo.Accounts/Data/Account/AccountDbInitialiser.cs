using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ThAmCo.Accounts.Data.Account
{
    public static class AccountDbInitialiser
    {
        public static async Task SeedTestData(AccountDbContext context, IServiceProvider services)
        {
            if (context.Users.Any())
                return;

            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            AppUser[] users = {
                new AppUser { UserName = "admin", Email = "admin@example.com", Forename = "Admin", Surname = "User" },
                new AppUser { UserName = "staff", Email = "staff@example.com", Forename = "Staff", Surname = "User" },
                new AppUser { UserName = "bob", Email = "bob@example.com", Forename = "Robert 'Bobby'", Surname = "Robertson" },
                new AppUser { UserName = "betty", Email = "betty@example.com", Forename = "Bethany 'Betty'", Surname = "Roberts" }
            };
            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password1_");
                // auto confirm email addresses for test users
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await userManager.ConfirmEmailAsync(user, token);
            }

            await userManager.AddToRolesAsync(users[0], new []{ "Admin", "Staff", "Customer" });
            await userManager.AddToRolesAsync(users[1], new[] { "Staff", "Customer" });
            await userManager.AddToRoleAsync(users[2], "Customer");
            await userManager.AddToRoleAsync(users[3], "Customer");
        }
    }
}
