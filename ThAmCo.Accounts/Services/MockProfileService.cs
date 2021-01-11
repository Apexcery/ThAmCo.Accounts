using System;
using System.Threading.Tasks;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.Profile;

namespace ThAmCo.Accounts.Services
{
    public class MockProfileService : IProfileService
    {
        public Task AddProfile(ProfileDto profile)
        {
            throw new NotImplementedException();
        }
    }
}
