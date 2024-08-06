using System.Collections.Generic;
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

namespace Dimo.Client.Core.Services.Events
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

                if (response.IsSuccessStatusCode)
                {
#if NETSTANDARD
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IReadOnlyCollection<DeviceEvent>>(json);
#elif NET6_0_OR_GREATER
                    return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<DeviceEvent>>(cancellationToken: cancellationToken);
#endif
                }

                throw new HttpRequestException(response.ReasonPhrase);
            }
        }
    }
}