using System.Net.Http;

namespace Dimo.Client.Core.Services.TokenExchange
{
    internal sealed class TokenExchangeService : ITokenExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TokenExchangeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}