
#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif

namespace Dimo.Client.Models
{
    public class WebhookDefinitionBase
    {
        /// <summary>
        /// The DIMO service that you're making the webhook event request for. At present, this will always be "Telemetry".
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonConverter(typeof(JsonStringEnumConverter))]
#else
[JsonConverter(typeof(StringEnumConverter))]
#endif
        public WebhookService Service { get; set; }

        /// <summary>
        /// The Telemetry field that the webhook will listen for events on, for a given Token ID.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The trigger that the webhook will be listening for. You can use the following condition statements: >=, <=, >, <, =
        /// You should always include valueNumber.
        /// </summary>
        public string Trigger { get; set; }

        /// <summary>
        /// Should be one of the following:
        /// - Realtime: continues firing as long as the condition remains true
        /// - Hourly: fires every hour as long as the condition remains true
        /// - Daily: fires every day as long as the condition remains true (Coming Soon)
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonConverter(typeof(JsonStringEnumConverter))]
#else
[JsonConverter(typeof(StringEnumConverter))]
#endif
        public WebhookSetup Setup { get; set; }

        /// <summary>
        /// A brief description of the webhook conditions for your own reference.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The endpoint URI that will receive POST requests from your webhook upon firing.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("target_uri")]
#else
        [JsonProperty("target_uri")]
#endif
        public string TargetUri { get; set; }

        /// <summary>
        /// Should be one of the following:
        /// - Active: the webhook is actively in use
        /// - Inactive: the webhook exists, but is not actively in use
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonConverter(typeof(JsonStringEnumConverter))]
#else
[JsonConverter(typeof(StringEnumConverter))]
#endif
        public WebhookStatus Status { get; set; }
    }
}