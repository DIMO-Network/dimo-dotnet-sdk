using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Events
{
    /// <summary>
    /// Service for managing vehicle events and webhooks in the DIMO platform.
    /// </summary>
    public interface IVehicleEventsService
    {
        /// <summary>
        /// Retrieves a list of all webhook configurations for the authenticated user.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>A collection of webhook configurations.</returns>
        Task<IReadOnlyCollection<WebhookDefinition>> ListWebhooksAsync(string authToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates a new webhook configuration.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="definitionWebhookDefinitionRequest">The webhook configuration to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>The created webhook definition.</returns>
        Task<WebhookDefinition> CreateWebhookAsync(string authToken, WebhookDefinitionRequest definitionWebhookDefinitionRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of available signal names and their units that can be used in webhook configurations.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>A collection of signal names and their units.</returns>
        Task<IReadOnlyCollection<WebhookSignal>> GetWebhookSignalNamesAsync(string authToken, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing webhook configuration.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to update.</param>
        /// <param name="definitionWebhookDefinitionRequest">The updated webhook configuration.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>The updated webhook configuration.</returns>
        Task<WebhookDefinition> UpdateWebhookAsync(string authToken, string webhookId, WebhookDefinitionRequest definitionWebhookDefinitionRequest, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a webhook configuration.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        Task DeleteWebhookAsync(string authToken, string webhookId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of vehicle token IDs that are subscribed to a specific webhook.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>A collection of vehicle token IDs.</returns>
        Task<IReadOnlyCollection<int>> ListSubscribedVehiclesAsync(string authToken, string webhookId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of webhook IDs that a specific vehicle is subscribed to.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="tokenId">The vehicle token ID.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        /// <returns>A collection of webhook IDs.</returns>
        Task<IReadOnlyCollection<VehicleSubscriptionDefinition>> ListVehicleSubscriptionsAsync(string authToken, int tokenId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes a specific vehicle to a webhook.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to subscribe to.</param>
        /// <param name="tokenId">The vehicle token ID to subscribe.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        Task SubscribeVehicleAsync(string authToken, string webhookId, int tokenId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Subscribes all vehicles to a webhook.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to subscribe to.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        Task SubscribeAllVehiclesAsync(string authToken, string webhookId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unsubscribes a specific vehicle from a webhook.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to unsubscribe from.</param>
        /// <param name="tokenId">The vehicle token ID to unsubscribe.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        Task UnsubscribeVehicleAsync(string authToken, string webhookId, int tokenId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unsubscribes all vehicles from a webhook.
        /// </summary>
        /// <param name="authToken">The developer token for API authentication.</param>
        /// <param name="webhookId">The ID of the webhook to unsubscribe from.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <exception cref="HttpRequestException">Thrown when the request fails.</exception>
        Task UnsubscribeAllVehiclesAsync(string authToken, string webhookId, CancellationToken cancellationToken = default);
    }
} 