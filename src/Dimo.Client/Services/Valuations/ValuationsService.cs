using System.Net.Http;

namespace Dimo.Client.Services.Valuations
{
    internal sealed class ValuationsService : IValuationsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ValuationsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
