#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dimo.Client.Services.Webhooks
{
    public class WebhooksService : IWebhooksService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public WebhooksService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IReadOnlyCollection<object>> GetAllWebhooksAsync(string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.GetAsync("/v1/webhooks", cancellationToken);
                
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<object>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<object>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task RegisterNewWebhookAsync(string url, string eventType, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var payload = JsonConvert.SerializeObject(new { url });
#elif NET6_0_OR_GREATER
                var payload = JsonSerializer.Serialize(new { url });
#endif
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/v1/webhooks", content, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<IReadOnlyCollection<string>> GetWebhookSignalNamesAsync(string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.GetAsync("/v1/webhooks/signals", cancellationToken);
                
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<string>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<string>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<IReadOnlyCollection<object>> GetSubscriptionsForVehicleAsync(string vehicleTokenId, string authToken,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.GetAsync($"/v1/webhooks/vehicles/{vehicleTokenId}", cancellationToken);
                
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<object>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<object>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<IReadOnlyCollection<string>> GetVehiclesSubscribedToWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                
                var response = await client.GetAsync($"/v1/webhooks/{webhookId}", cancellationToken);
                
                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<string>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<string>>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task UpdateWebhookAsync(string webhookId, string url, string eventType, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var payload = JsonConvert.SerializeObject(new { url });
#elif NET6_0_OR_GREATER
                var payload = JsonSerializer.Serialize(new { url });
#endif
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"/v1/webhooks/{webhookId}", content, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.DeleteAsync($"/v1/webhooks/{webhookId}", cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task SubscribeAllSharedVehiclesToWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.PostAsync($"/v1/webhooks/{webhookId}/subscribe/all", null, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task AssignVehicleToWebhookAsync(string webhookId, string vehicleTokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync($"/v1/webhooks/{webhookId}/subscribe/{vehicleTokenId}", null, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task UnsubscribeAllSharedVehiclesFromWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.PostAsync($"/v1/webhooks/{webhookId}/unsubscribe/all", null, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task UnassignVehicleFromWebhookAsync(string webhookId, string vehicleTokenId, string authToken, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Webhooks))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.PostAsync($"/v1/webhooks/{webhookId}/unsubscribe/{vehicleTokenId}", null, cancellationToken);
                
                response.EnsureSuccessStatusCode();
            }
        }
    }
}