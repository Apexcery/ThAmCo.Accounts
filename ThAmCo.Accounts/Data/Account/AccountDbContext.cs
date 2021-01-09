using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Accounts.Data.Account
{
    public class AccountDbContext : IdentityDbContext<AppUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Descriptor = "ThAmCo Administrators"
                },
                new AppRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    Descriptor = "ThAmCo Staff Members"
                },
                new AppRole
                {
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    Descriptor = "ThAmCo Customers"
                }
            );
        }
    }
}
