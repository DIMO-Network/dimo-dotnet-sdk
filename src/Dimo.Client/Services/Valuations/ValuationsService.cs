using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;
#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif

namespace Dimo.Client.Services.Valuations
{
    internal sealed class ValuationsService : IValuationsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ValuationsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IReadOnlyCollection<Valuation>> GetValuationsAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"/v1/user/devices/{tokenId}/valuations", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<Valuation>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Valuation>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task GetInstantOfferAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.User))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.PostAsync($"/v1/vehicles/{tokenId}/instant-offer", null, cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<IReadOnlyCollection<OfferData>> GetOffersAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync($"/v1/user/devices/{tokenId}/offers", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<OfferData>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<OfferData>>(cancellationToken: cancellationToken);
#endif
            }
        }
    }
}
