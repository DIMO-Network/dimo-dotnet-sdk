using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dimo.Client.Services.Webhooks
{
    public interface IWebhooksService
    {
        Task<IReadOnlyCollection<object>> GetAllWebhooksAsync(string authToken, CancellationToken cancellationToken = default);
        Task RegisterNewWebhookAsync(string url, string eventType, string authToken, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<string>> GetWebhookSignalNamesAsync(string authToken, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<object>> GetSubscriptionsForVehicleAsync(string vehicleTokenId, string authToken, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<string>> GetVehiclesSubscribedToWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default);
        Task UpdateWebhookAsync(string webhookId, string url, string eventType, string authToken, CancellationToken cancellationToken = default);
        Task DeleteWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default);
        Task SubscribeAllSharedVehiclesToWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default);
        Task AssignVehicleToWebhookAsync(string webhookId, string vehicleTokenId, string authToken, CancellationToken cancellationToken = default);
        Task UnsubscribeAllSharedVehiclesFromWebhookAsync(string webhookId, string authToken, CancellationToken cancellationToken = default);
        Task UnassignVehicleFromWebhookAsync(string webhookId, string vehicleTokenId, string authToken, CancellationToken cancellationToken = default);
    }
}