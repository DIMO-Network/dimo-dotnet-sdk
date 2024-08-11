#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Trips
{
    internal sealed class TripsService : ITripsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TripsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TripHistory> ListTripsByTokenIdAsync(string tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/vehicle/{tokenId}/trips", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TripHistory>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<TripHistory>(cancellationToken: cancellationToken);
#endif
            }
        }
    }
}
