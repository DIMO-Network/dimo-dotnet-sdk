#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.DeviceDefinitions
{
    internal sealed class DeviceDefinitionService : IDeviceDefinitionsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeviceDefinitionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DeviceDefinition> GetByMmyAsync(string make, string model, int year, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceDefinitions))
            {
                var response = await client.GetAsync($"/device-definitions?make={make}&model={model}&year={year}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceDefinition>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceDefinition>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<DeviceDefinition> GetByIdAsync(string deviceDefinitionId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceDefinitions))
            {
                var response = await client.GetAsync($"/device-definitions/{deviceDefinitionId}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceDefinition>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceDefinition>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<IReadOnlyCollection<DeviceMake>> GetDeviceMakesAsync(CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceDefinitions))
            {
                var response = await client.GetAsync($"/device-makes", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<DeviceMake>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DeviceMake>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<DeviceType> GetDeviceTypeByIdAsync(string deviceTypeId, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.DeviceDefinitions))
            {
                var response = await client.GetAsync($"/device-types/{deviceTypeId}", cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DeviceType>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<DeviceType>(cancellationToken: cancellationToken);
#endif
            }
        }
    }
}
