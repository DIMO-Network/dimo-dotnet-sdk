using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;
#if NETSTANDARD
using System.Text;
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

        public async Task<CreatedVehicle> CreateVehicleAsync(string countryCode, string deviceDefinitionId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { countryCode, code, redirectUri }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, code, redirectUri }));
#endif
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
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { countryCode, vin, canProtocol }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, vin, canProtocol }));
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
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { vin, signature }), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"v1/user/devices/{userDeviceId}/vin")
                {
                    Content = content
                };
                var response = await client.SendAsync(request, cancellationToken);
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { vin, signature }));
                var response = await client.PatchAsync($"v1/user/devices/{userDeviceId}/vin", content, cancellationToken);
#endif
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
        }

        public async Task<ClaimingPayload> GetClaimingPayloadAsync(string serial, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task SignClaimingPayloadAsync(string serial, string aftermarketDeviceSignature, string userSignature, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, vin, canProtocol }));
#endif
                var response = await client.PostAsync($"/v1/aftermarket/device/by-serial/{serial}/commands/claim", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<ClaimingPayload> GetMintingPayloadAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, userSignature }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { countryCode, vin, canProtocol }));
#endif
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/mint", content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task OptInShareDataAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/opt-in",  null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return; 
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task RefreshSmartCarDataAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.PostAsync($"/v1/user/devices/{userDeviceId}/commands/refresh", null, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task GetPairingPayloadAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/v1/user/devices/{userDeviceId}/aftermarket/commands/pair", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task SignPairingPayloadAsync(string userDeviceId, int[] aftermarketDeviceSignature,
            string externalId, int[] signature, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content =
                    new StringContent(JsonConvert.SerializeObject(new { aftermarketDeviceSignature, externalId, signature }),
                        Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { aftermarketDeviceSignature, externalId, signature }));
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

        public async Task<CommandResponse> LockDoorsAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<CommandResponse> UnlockDoorsAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<CommandResponse> OpenFrunkAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<CommandResponse> OpenTrunkAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<AvailableErrorCode> ListErrorCodesAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<AvailableErrorCode> SubmitErrorCodesAsync(string userDeviceId, string[] errorCodes, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new { errorCodes }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new { errorCodes }));
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

        public async Task<QueryResult> ClearErrorCodesAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<AftermarketDevice> GetAftermarketDeviceAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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

        public async Task<Stream> GetAftermarketDeviceImageAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/v1/aftermarket/device/{tokenId}/image", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<AftermarketDevice> GetAftermarketDeviceMetadataByAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
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
