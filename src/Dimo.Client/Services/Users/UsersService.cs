using System.Net.Http;

namespace Dimo.Client.Services.Users
{
    internal sealed class UsersService : IUsersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UsersService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}