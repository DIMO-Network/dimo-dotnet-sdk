using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;
#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif

namespace Dimo.Client.Core.Services.Trips
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

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TripHistory>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<TripHistory>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
