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

namespace Dimo.Client.Services.Events
{
    internal sealed class EventsService : IEventsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EventsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IReadOnlyCollection<DeviceEvent>> ListEventsAsync(string authToken, string deviceId = null, string deviceType = null, string subType = null,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync($"/v1/event?device_id={deviceId}&type={deviceType}&sub_type={subType}", cancellationToken);
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<DeviceEvent>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DeviceEvent>>(cancellationToken: cancellationToken);
#endif

            }
        }
    }
}