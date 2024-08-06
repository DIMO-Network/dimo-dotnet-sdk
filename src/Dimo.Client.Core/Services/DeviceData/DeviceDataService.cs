using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif


namespace Dimo.Client.Core.Services.DeviceData
{
    internal sealed class DeviceDataService : IDeviceDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeviceDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<VehicleHistory> GetVehicleHistoryAsync(long tokenId, string authToken, string startDate = null, string endDate = null,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                
                var response = await client.GetAsync($"/v2/vehicle/{tokenId}/history?startDate={startDate}&endDate={endDate}", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<VehicleHistory>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<VehicleHistory>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<VehicleStatus> GetVehicleStatusAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                
                var response = await client.GetAsync($"/v1/vehicle/{tokenId}/status", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<VehicleStatus>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<VehicleStatus>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<RawVehicleStatus> GetRawVehicleStatusAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/vehicle/{tokenId}/status-raw", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RawVehicleStatus>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<RawVehicleStatus>(cancellationToken: cancellationToken);
#endif
                }
                
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<VehicleStatus> GetUserDeviceStatusAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/device-data/{userDeviceId}/status", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<VehicleStatus>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<VehicleStatus>(cancellationToken: cancellationToken);
#endif
                }
                
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<VehicleHistory> GetUserDeviceHistoryAsync(string userDeviceId, string authToken, string startDate = null, string endDate = null,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/device-data/{userDeviceId}/historical", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<VehicleHistory>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<VehicleHistory>(cancellationToken: cancellationToken);
#endif
                }
                
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }

        public async Task<ExportDataResponse> ExportUserDeviceDataAsync(string userDeviceId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceData))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/user/device-data/{userDeviceId}/export/json/email", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ExportDataResponse>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<ExportDataResponse>(cancellationToken: cancellationToken);
#endif
                }
                
                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}
