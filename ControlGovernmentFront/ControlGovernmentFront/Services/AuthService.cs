using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ControlGovernmentFront.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var loginDto = new { Email = email, Password = password };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:44331/api/auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var tokenData = JsonSerializer.Deserialize<TokenResponse>(responseData);
                return tokenData?.Token;
            }
            else
            {
                throw new HttpRequestException("Invalid login attempt.");
            }
        }

        private class TokenResponse
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }
    }
}
