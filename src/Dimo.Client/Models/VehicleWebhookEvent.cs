using System;

namespace Dimo.Client.Models
{
    public class VehicleWebhookEvent
    {
        public int TokenId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public int ValueNumber { get; set; }
        public string ValueString { get; set; }
        public string Source { get; set; }
        public string Producer { get; set; }
        public string CloudEventId { get; set; }
    }
}