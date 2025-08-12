#if NET6_0_OR_GREATER
using System.Text.Json.Serialization;

#else
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif

namespace Dimo.Client.Models
{
    public class WebhookDefinitionRequest : WebhookDefinitionBase
    {
        /// <summary>
        /// A plain/text string that must be returned by your target_uri webhook listener when creating or updating a webhook for verification purposes.
        /// </summary>
#if NET6_0_OR_GREATER
        [JsonPropertyName("verification_token")]
#else
        [JsonProperty("verification_token")]
#endif
        public string VerificationToken { get; set; }
    }
}