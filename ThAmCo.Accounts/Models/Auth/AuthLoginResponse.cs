using Newtonsoft.Json;

namespace ThAmCo.Accounts.Models.Auth
{
    public class AuthLoginResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public string Scope { get; set; }
    }
}
