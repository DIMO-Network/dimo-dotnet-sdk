using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;
#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif

namespace Dimo.Client.Services.Users
{
    internal sealed class UsersService : IUsersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<User> GetUserAsync(string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<User> UpdateUserAsync(string authToken, User user, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
#endif
                var response = await client.PutAsync("/v1/user", content, cancellationToken);
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<User>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task DeleteUserAsync(string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.DeleteAsync($"/v1/user", cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task SendConfirmationEmailAsync(string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/user/send-confirmation-email", null, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task ConfirmEmailAsync(string authToken, string confirmationCode, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    key = confirmationCode
                }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new
                {
                    key = confirmationCode
                }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/user/confirm-email", content, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}