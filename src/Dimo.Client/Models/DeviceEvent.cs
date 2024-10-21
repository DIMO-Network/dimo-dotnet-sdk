using System;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class DeviceEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string UserId { get; set; }
        public object DeviceId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public EventData Data { get; set; }
    }

    public class EventData
    {
        public string Method { get; set; }
        public string UserId { get; set; }
        public string Timestamp { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceEvent(string Id, string Type, string SubType, string UserId, object DeviceId, DateTimeOffset Timestamp, EventData Data);
    
    public record EventData(string Method, string UserId, string Timestamp);
#endif
}