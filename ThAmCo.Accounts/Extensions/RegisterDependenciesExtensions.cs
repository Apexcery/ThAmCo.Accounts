using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Mapper;
using ThAmCo.Accounts.Repositories;
using ThAmCo.Accounts.Services;

namespace ThAmCo.Accounts.Extensions
{
    public static class RegisterDependenciesExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.RegisterAccounts(env);

            services.RegisterProfiles(env, config);

            services.RegisterMapper();

            return services;
        }

        private static IServiceCollection RegisterAccounts(this IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                services.AddSingleton<IAccountsRepository, MockAccountsRepository>();
            else
                services.AddScoped<IAccountsRepository, AccountsRepository>();

            return services;
        }

        private static IServiceCollection RegisterProfiles(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
                services.AddSingleton<IProfileService, MockProfileService>();
            else
                services.AddHttpClient<IProfileService, ProfileService>(options =>
                {
                    options.BaseAddress = new Uri(config["AppSettings:Endpoints:ProfilesEndpoint"]);
                });

            return services;
        }

        private static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AccountMapper));

            return services;
        }
    }
}
