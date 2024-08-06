using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;
using System.Text;
#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif

namespace Dimo.Client.Core.Services.Devices
{
    internal sealed class DevicesService : IDevicesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DevicesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CreatedVehicle> CreateVehicleAsync(string countryCode, string deviceDefinitionId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { countryCode, deviceDefinitionId }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, deviceDefinitionId }));
#endif
                var response = await client.PostAsync($"/v1/user/devices", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CreatedVehicle>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CreatedVehicle>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CreatedVehicle> CreateVehicleFromSmartCarAsync(string code, string countryCode, string redirectUri,
            string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var payload = JsonConvert.SerializeObject(new { countryCode, code, redirectUri });
#elif NET6_0_OR_GREATER
                var payload = JsonSerializer.Serialize(new { countryCode, code, redirectUri });
#endif
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"/v1/user/devices/fromsmartcar", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CreatedVehicle>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CreatedVehicle>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CreatedVehicle> CreateVehicleFromVinAsync(string vin, string countryCode, string canProtocol,
            string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { countryCode, vin, canProtocol }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, vin, canProtocol }), Encoding.UTF8, "application/json");
#endif
                
                var response = await client.PostAsync($"/v1/user/devices/fromvin", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CreatedVehicle>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CreatedVehicle>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task UpdateVehicleVinAsync(string userDeviceId, string vin, string signature,
            string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { vin, signature }), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"v1/user/devices/{userDeviceId}/vin")
                {
                    Content = content
                };
                var response = await client.SendAsync(request, cancellationToken);
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { vin, signature }), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync($"v1/user/devices/{userDeviceId}/vin", content, cancellationToken);
#endif
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
        }

        public async Task<ClaimingPayload> GetClaimingPayloadAsync(string serial, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/aftermarket/device/by-serial/{serial}/commands/claim", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClaimingPayload>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<ClaimingPayload>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task SignClaimingPayloadAsync(string serial, string aftermarketDeviceSignature, string userSignature, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/aftermarket/device/by-serial/{serial}/commands/claim", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<ClaimingPayload> GetMintingPayloadAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/devices/{userDeviceId}/commands/mint", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ClaimingPayload>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<ClaimingPayload>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task SignMintingPayloadAsync(string userDeviceId, string aftermarketDeviceSignature, string userSignature,
            string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/mint", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task OptInShareDataAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/opt-in",  null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return; 
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task RefreshSmartCarDataAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/refresh", null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task GetPairingPayloadAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/devices/{userDeviceId}/aftermarket/commands/pair", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task SignPairingPayloadAsync(string userDeviceId, int[] aftermarketDeviceSignature,
            string externalId, int[] signature, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content =
                    new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, externalId, signature }),
                        Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { aftermarketDeviceSignature, externalId, signature }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/aftermarket/commands/pair", content,
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CommandResponse> LockDoorsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/vehicle/{tokenId}/commands/doors/lock",null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CommandResponse>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CommandResponse>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CommandResponse> UnlockDoorsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/vehicle/{tokenId}/commands/doors/unlock", null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CommandResponse>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CommandResponse>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CommandResponse> OpenFrunkAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/vehicle/{tokenId}/commands/frunk/open", null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CommandResponse>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CommandResponse>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<CommandResponse> OpenTrunkAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/vehicle/{tokenId}/commands/trunk/open", null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CommandResponse>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<CommandResponse>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AvailableErrorCode> ListErrorCodesAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/devices/{userDeviceId}/error-codes", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AvailableErrorCode>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<AvailableErrorCode>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AvailableErrorCode> SubmitErrorCodesAsync(string userDeviceId, string[] errorCodes, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { errorCodes }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { errorCodes }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/error-codes", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<AvailableErrorCode>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<AvailableErrorCode>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<QueryResult> ClearErrorCodesAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/error-codes/clear",  null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<QueryResult>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<QueryResult>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AftermarketDevice> GetAftermarketDeviceAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/aftermarket/device/{tokenId}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AftermarketDevice>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<AftermarketDevice>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<Stream> GetAftermarketDeviceImageAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/aftermarket/device/{tokenId}/image", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    return await response.Content.ReadAsStreamAsync();
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadAsStreamAsync(cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AftermarketDevice> GetAftermarketDeviceMetadataByAddressAsync(string address, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/aftermarket/device/by-address/{address}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AftermarketDevice>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<AftermarketDevice>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
