using System;

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class VehicleStatus
    {
        public decimal FuelPercentRemaining { get; set; }
        public decimal Odometer { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTimeOffset RecordUpdatedAt { get; set; }
        public DateTimeOffset RecordCreatedAt { get; set; }
        public decimal BatteryVoltage { get; set; }
        public int AmbientTemperature { get; set; }
    }    
#elif NET6_0_OR_GREATER
    public record VehicleStatus
    {
        public decimal FuelPercentRemaining { get; init; }
        public decimal Odometer { get; init; }
        public decimal Latitude { get; init; }
        public decimal Longitude { get; init; }
        public DateTimeOffset RecordUpdatedAt { get; init; }
        public DateTimeOffset RecordCreatedAt { get; init; }
        public decimal BatteryVoltage { get; init; }
        public int AmbientTemperature { get; init; }
    }
#endif
}