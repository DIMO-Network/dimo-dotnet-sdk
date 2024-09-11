#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.VehicleSignalDecoding
{
    internal sealed class VehicleSignalDecodingService : IVehicleSignalDecodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public VehicleSignalDecodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DeviceConfigUrls> ListConfigUrlsByVinAsync(string vin, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/vin/{vin}/urls", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceConfigUrls>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceConfigUrls>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<PidConfig> GetPidConfigsAsync(string templateName, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/pids/{templateName}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PidConfig>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<PidConfig>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<DeviceSetting> GetDeviceSettingsAsync(string templateName, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/pids/{templateName}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceSetting>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceSetting>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<string> GetDbcTextAsync(string templateName, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/dbc/{templateName}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                return await response.Content.ReadAsStringAsync();
#elif NET6_0_OR_GREATER
                return await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<DeviceStatus> GetDeviceStatusByAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/eth-addr/{address}/status", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceStatus>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceStatus>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task SetDeviceStatusByAddressAsync(string address, DeviceStatusConfig newDeviceStatus,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(newDeviceStatus), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"/v1/device-config/eth-addr/{address}/status")
                {
                    Content = content
                };
                var response = await client.SendAsync(request, cancellationToken);
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(newDeviceStatus), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync($"/v1/device-config/eth-addr/{address}/status", content,  cancellationToken);
#endif
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task GetJobsByAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/eth-addr/{address}/status", cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task GetPendingJobsByAddressAsync(string address, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/eth-addr/{address}/jobs/pending", cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task SetJobStatusByAddressAsync(string address, string jobId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.VehicleSignalDecoding))
            {
                var response = await client.GetAsync($"/v1/device-config/eth-addr/{address}/jobs/{jobId}/{{status}}", cancellationToken);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
