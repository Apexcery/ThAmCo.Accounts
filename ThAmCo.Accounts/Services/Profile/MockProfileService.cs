using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.Profile;

namespace ThAmCo.Accounts.Services.Profile
{
    public class MockProfileService : IProfileService
    {
        public static List<ProfileDto> Profiles = new List<ProfileDto>();

        public async Task AddProfile(ProfileDto profile)
        {
            Profiles.Add(profile);
        }
    }
}
