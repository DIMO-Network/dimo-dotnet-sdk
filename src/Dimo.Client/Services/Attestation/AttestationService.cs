using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

#if NETSTANDARD
using Newtonsoft.Json;
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif

namespace Dimo.Client.Services.Attestation
{
    public sealed class AttestationService : IAttestationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AttestationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<VehicleVinVc> CreateVinVcAsync(long tokenId, string vehicleToken,
            CancellationToken cancellationToken = default)
        {
            if (tokenId <= 0)
            {
                throw new ArgumentException("TokenId must be greater than 0", nameof(tokenId));
            }
            
            if (string.IsNullOrWhiteSpace(vehicleToken))
                throw new ArgumentException("Vehicle token must not be null or empty", nameof(vehicleToken)); 


            const string path = "/v1/vc/vin/{0}";

            using (var client = _httpClientFactory.CreateClient(ApiNames.Attestation))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", vehicleToken);

                string requestUri = string.Format(path, tokenId);

                var response = await client.PostAsync(requestUri, null, cancellationToken).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<VehicleVinVc>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<VehicleVinVc>(cancellationToken: cancellationToken).ConfigureAwait(false);
#endif
            }
        }
    }
}