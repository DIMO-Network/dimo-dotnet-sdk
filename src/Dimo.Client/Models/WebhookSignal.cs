#if NETSTANDARD
using Newtonsoft.Json;
#elif NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Models
{
    public class WebhookSignal
    {
        public string Name { get; set; }
        public string Unit { get; set; }
    }
} 