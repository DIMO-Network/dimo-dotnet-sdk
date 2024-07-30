using System.Net.Http;

namespace Dimo.Client.Core.Services.Trips
{
    internal sealed class TripsService : ITripsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TripsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
