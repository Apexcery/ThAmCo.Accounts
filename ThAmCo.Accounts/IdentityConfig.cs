using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

using static IdentityServer4.IdentityServerConstants;

namespace ThAmCo.Accounts
{
    public static class IdentityConfigurationExtensions
    {
        public static IEnumerable<IdentityResource> GetIdentityResources(this IConfiguration configuration)
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                new IdentityResources.Profile(),

                new IdentityResource
                {
                    Name = "roles",
                    Description = "All roles",
                    UserClaims = { ClaimTypes.Role }
                }
            };
        }

        public static IEnumerable<ApiResource> GetIdentityApis(this IConfiguration configuration)
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "thamco_api",
                    Description = "ThAmCo API's",
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "thamco_api",
                            DisplayName = "Full access to ThAmCo API's"
                        }
                    },
                    UserClaims = { ClaimTypes.Role }
                }
            };
        }

        public static IEnumerable<Client> GetIdentityClients(this IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "thamcoApiClient",
                    ClientName = "ThAmCo API Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = (int) TimeSpan.FromDays(1).TotalSeconds,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireClientSecret = false,
                    AllowedScopes =
                    {
                        "thamco_api",
                        "roles",
                        StandardScopes.OpenId,
                        StandardScopes.Profile
                    }
                }
            };
        }
    }
}
