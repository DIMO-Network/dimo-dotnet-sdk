using System;

#if NETSTANDARD
using Newtonsoft.Json;
#elif NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class VehicleVinVcLatest
    {
        public long VehicleTokenId { get; set; }
        public string Vin { get; set; }
        public string RecordedBy { get; set; }
        public DateTime RecordedAt { get; set; }
        public string CountryCode { get; set; }
        public string VehicleContractAddress { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        
        [JsonProperty("rawVC")]
        public string RawVc { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record VehicleVinVcLatest
    {
        public long VehicleTokenId { get; init; }
        public string Vin { get; init; }
        public string RecordedBy { get; init; }
        public DateTime RecordedAt { get; init; }
        public string CountryCode { get; init; }
        public string VehicleContractAddress { get; init; }
        public DateTime ValidFrom { get; init; }
        public DateTime ValidTo { get; init; }
        
        [JsonPropertyName("rawVC")]
        public string RawVc { get; init; }
    }
#endif
}