using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using ThAmCo.Accounts.Interfaces;
using ThAmCo.Accounts.Models.Profile;

namespace ThAmCo.Accounts.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _client;

        public ProfileService(HttpClient client)
        {
            _client = client;
        }

        public async Task AddProfile(ProfileDto profile)
        {
            var content = new ObjectContent(typeof(ProfileDto), profile, new JsonMediaTypeFormatter());

            await _client.PostAsync("", content);
        }
    }
}
