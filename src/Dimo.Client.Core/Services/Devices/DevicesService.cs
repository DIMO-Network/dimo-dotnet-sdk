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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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

        public async Task UpdateVehicleVinAsync(string userDeviceId, UpdateVinRequest payload,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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

        public async Task SignClaimingPayloadAsync(string serial, SignClaimRequest payload, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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

        public async Task SignMintingPayloadAsync(string userDeviceId, SignMintRequest payload,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task SignPairingPayloadAsync(string userDeviceId, string signature, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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

        public async Task<AftermarketDevice> GetAftermarketDeviceAsync(string userDeviceId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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

        public async Task<AftermarketDevice> GetAftermarketDeviceMetadataByAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Devices))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

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
