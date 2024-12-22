using System;

#if NETSTANDARD
using Newtonsoft.Json;
#elif NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Models
{
#if NETSTANDARD
public class VinVcLatestScheme<T>
    {
        [JsonProperty("vinVCLatest")]
        public T VinVcLatest { get; set; }
    }

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

    public record VinVcLatestScheme<T>(T VinVcLatest);

    public record VehicleVinVcLatest(
        long VehicleTokenId,
        string Vin,
        string RecordedBy,
        DateTime RecordedAt,
        string CountryCode,
        string VehicleContractAddress,
        DateTime ValidFrom,
        DateTime ValidTo,
        string RawVc);

#endif
}