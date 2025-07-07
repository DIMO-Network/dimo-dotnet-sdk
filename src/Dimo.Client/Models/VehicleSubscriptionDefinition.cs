#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace Dimo.Client.Models
{
    public class VehicleSubscriptionDefinition
    {
        /// <summary>
        /// The unique identifier for the event.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("event_id")]
#else
        [JsonProperty("event_id")]
#endif
        public string EventId { get; set; }

        /// <summary>
        /// The token ID of the vehicle that is subscribed to the webhook.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("vehicle_token_id")]
#else
        [JsonProperty("vehicle_token_id")]
#endif
        public string VehicleTokenId { get; set; }

        /// <summary>
        /// The UTC timestamp when the subscription was created.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("created_at")]
#else
        [JsonProperty("created_at")]
#endif
        public string CreatedAt { get; set; }

        /// <summary>
        /// A brief description of the webhook conditions for your own reference.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("description")]
#else
        [JsonProperty("description")]
#endif
        public string Description { get; set; }
    }
} 