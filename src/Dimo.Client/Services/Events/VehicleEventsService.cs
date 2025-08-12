#if NETSTANDARD
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Extensions;
using Dimo.Client.Models;
using Newtonsoft.Json.Linq;

namespace Dimo.Client.Services.Events
{
    internal sealed class VehicleEventsService : IVehicleEventsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string BasePath = "/v1/webhooks";
        private const string SignalsPath = BasePath + "/signals";
        private const string VehiclesPath = BasePath + "/vehicles";
        private const string SubscribePath = BasePath + "/{0}/subscribe";
        private const string UnsubscribePath = BasePath + "/{0}/unsubscribe";

        public VehicleEventsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IReadOnlyCollection<WebhookDefinition>> ListWebhooksAsync(string authToken,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync(BasePath, cancellationToken);
                
                await response.ThrowIfFailedAsync();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<WebhookDefinition>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<WebhookDefinition>>(
                    cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<WebhookDefinition> CreateWebhookAsync(string authToken,
            WebhookDefinitionRequest definitionWebhookDefinitionRequest,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(definitionWebhookDefinitionRequest));
                var response = await client.PostAsync(BasePath, content, cancellationToken);
#elif NET6_0_OR_GREATER
                var response = await client.PostAsJsonAsync(BasePath, definitionWebhookDefinitionRequest, cancellationToken);
#endif
                await response.ThrowIfFailedAsync();
                
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                var webhookDefinition = JsonConvert.DeserializeObject<WebhookDefinition>(json);
#elif NET6_0_OR_GREATER
                var webhookDefinition = await response.Content.ReadFromJsonAsync<WebhookDefinition>(cancellationToken: cancellationToken);
#endif
                webhookDefinition.Service = definitionWebhookDefinitionRequest.Service;
                webhookDefinition.Data = definitionWebhookDefinitionRequest.Data;
                webhookDefinition.Trigger = definitionWebhookDefinitionRequest.Trigger;
                webhookDefinition.Setup = definitionWebhookDefinitionRequest.Setup;
                webhookDefinition.Description = definitionWebhookDefinitionRequest.Description;
                webhookDefinition.TargetUri = definitionWebhookDefinitionRequest.TargetUri;
                webhookDefinition.Status = definitionWebhookDefinitionRequest.Status;
                
                return webhookDefinition;
            }
        }

        public async Task<IReadOnlyCollection<WebhookSignal>> GetWebhookSignalNamesAsync(string authToken,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync(SignalsPath, cancellationToken);
                await response.ThrowIfFailedAsync();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<WebhookSignal>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<WebhookSignal>>(
                    cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<WebhookDefinition> UpdateWebhookAsync(string authToken, string webhookId,
            WebhookDefinitionRequest definitionWebhookDefinitionRequest, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(definitionWebhookDefinitionRequest));
                var response =
                    await client.PutAsync(string.Format(BasePath + "/{0}", webhookId), content, cancellationToken);
#elif NET6_0_OR_GREATER
                var response =
                    await client.PutAsJsonAsync(string.Format(BasePath + "/{0}", webhookId), definitionWebhookDefinitionRequest,
                    cancellationToken);
#endif
                await response.ThrowIfFailedAsync();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                var webhookDefinition = JsonConvert.DeserializeObject<WebhookDefinition>(json);
#elif NET6_0_OR_GREATER
                var webhookDefinition = await response.Content.ReadFromJsonAsync<WebhookDefinition>(
                    cancellationToken: cancellationToken);
#endif
                webhookDefinition.Service = definitionWebhookDefinitionRequest.Service;
                webhookDefinition.Data = definitionWebhookDefinitionRequest.Data;
                webhookDefinition.Trigger = definitionWebhookDefinitionRequest.Trigger;
                webhookDefinition.Setup = definitionWebhookDefinitionRequest.Setup;
                webhookDefinition.Description = definitionWebhookDefinitionRequest.Description;
                webhookDefinition.TargetUri = definitionWebhookDefinitionRequest.TargetUri;
                webhookDefinition.Status = definitionWebhookDefinitionRequest.Status;

                return webhookDefinition;
            }
        }

        public async Task DeleteWebhookAsync(string authToken, string webhookId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.DeleteAsync(string.Format(BasePath + "/{0}", webhookId), cancellationToken);
                await response.ThrowIfFailedAsync();
            }
        }

        public async Task<IReadOnlyCollection<int>> ListSubscribedVehiclesAsync(string authToken, string webhookId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync(string.Format(BasePath + "/{0}", webhookId),
                    cancellationToken);
                await response.ThrowIfFailedAsync();
                
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<int>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<int>>(
                    cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<IReadOnlyCollection<VehicleSubscriptionDefinition>> ListVehicleSubscriptionsAsync(
            string authToken, int tokenId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.GetAsync(string.Format(VehiclesPath + "/{0}", tokenId),
                    cancellationToken);
                
                await response.ThrowIfFailedAsync();
                
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IReadOnlyCollection<VehicleSubscriptionDefinition>>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<VehicleSubscriptionDefinition>>(
                    cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task SubscribeVehicleAsync(string authToken, string webhookId, int tokenId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync(string.Format(SubscribePath + "/{1}", webhookId, tokenId), null,
                    cancellationToken);

                await response.ThrowIfFailedAsync();
            }
        }

        public async Task SubscribeAllVehiclesAsync(string authToken, string webhookId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.PostAsync(string.Format(SubscribePath + "/all", webhookId), null,
                    cancellationToken);

                await response.ThrowIfFailedAsync();
            }
        }

        public async Task UnsubscribeVehicleAsync(string authToken, string webhookId, int tokenId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.DeleteAsync(string.Format(UnsubscribePath + "/{1}", webhookId, tokenId),
                    cancellationToken);
                
                await response.ThrowIfFailedAsync();
            }
        }

        public async Task UnsubscribeAllVehiclesAsync(string authToken, string webhookId,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Events))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
                var response = await client.DeleteAsync(string.Format(UnsubscribePath + "/all", webhookId),
                    cancellationToken);
                
                await response.ThrowIfFailedAsync();
            }
        }
    }
}