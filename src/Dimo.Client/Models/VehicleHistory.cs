using System;
#if NETSTANDARD
#elif NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Models
{
    #if NETSTANDARD
    public class VehicleHistory
    {
        public int Took { get; set; }
        public bool TimedOut { get; set; }
        public Shard Shards { get; set; }
        public QueryHit Hits { get; set; }
    }
    
    public class Shard
    {
        public int Total { get; set; }
        public int Successful { get; set; }
        public int Skipped { get; set; }
        public int Failed { get; set; }
    }

    public class QueryHit
    {
        public Total Total { get; set; }
        public decimal MaxScore { get; set; }
        public DataHit[] Hits { get; set; }
    }

    public class Total
    {
        public int Value { get; set; }
        public string Relation { get; set; }
    }

    public class DataHit
    {
        public string Index { get; set; }
        public string Id { get; set; }
        public decimal Score { get; set; }
        public Source Source { get; set; }
    }

    public class Source
    {
        public Data Data { get; set; }
        public string Subject { get; set; }
        public string SpecVersion { get; set; }
        public Location Location { get; set; }
        public string Id { get; set; }
        public string Path { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    
    public class Lora
    {
        public string DevAddr { get; set; }
        public int DcBalance { get; set; }
        public string FPort { get; set; }
        public string AppEui { get; set; }
        public string PayloadSize { get; set; }
        public string Fcnt { get; set; }
        public string DevEui { get; set; }
    }

    public class Data
    {
        public Lora Lora { get; set; }
        public int AmbientTemp { get; set; }
        public decimal Odometer { get; set; }
        public int Year { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string DeviceDefinitionId { get; set; }
        public int Nsat { get; set; }
        public string VinLast8 { get; set; }
        public int Speed { get; set; }
        public string MakeSlug { get; set; }
        public int BarometricPressure { get; set; }
        public decimal FuelPercentRemaining { get; set; }
        public string ModelSlug { get; set; }
        public decimal EngineLoad { get; set; }
        public string Model { get; set; }
        public string Region { get; set; }
        public string Make { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Range { get; set; }
    }
    
    #elif NET6_0_OR_GREATER
    public record VehicleHistory
    {
        [JsonPropertyName("took")]
        public int Took { get; init; }
        public bool TimedOut { get; init; }
        public Shard Shards { get; init; }
        public QueryHit Hits { get; init; }
    }
    
    public record Shard
    {
        public int Total { get; init; }
        public int Successful { get; init; }
        public int Skipped { get; init; }
        public int Failed { get; init; }
    }
    
    public record QueryHit
    {
        public Total Total { get; init; }
        public decimal MaxScore { get; init; }
        public DataHit[] Hits { get; init; }
    }
    
    public record Total
    {
        public int Value { get; init; }
        public string Relation { get; init; }
    }
    
    public record DataHit
    {
        public string Index { get; init; }
        public string Id { get; init; }
        public decimal Score { get; init; }
        public Source Source { get; init; }
    }
    
    public record Source
    {
        public Data Data { get; init; }
        public string Subject { get; init; }
        public string SpecVersion { get; init; }
        public Location Location { get; init; }
        public string Id { get; init; }
        public string Path { get; init; }
        public DateTime Time { get; init; }
        public string Type { get; init; }
    }
    
    public record Location
    {
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
    
    public record Lora
    {
        public string DevAddr { get; init; }
        public int DcBalance { get; init; }
        public string FPort { get; init; }
        public string AppEui { get; init; }
        public string PayloadSize { get; init; }
        public string Fcnt { get; init; }
        public string DevEui { get; init; }
    }
    
    public record Data
    {
        public Lora Lora { get; init; }
        public int AmbientTemp { get; init; }
        public decimal Odometer { get; init; }
        public int Year { get; init; }
        public decimal Latitude { get; init; }
        public decimal Longitude { get; init; }
        public string DeviceDefinitionId { get; init; }
        public int Nsat { get; init; }
        public string VinLast8 { get; init; }
        public int Speed { get; init; }
        public string MakeSlug { get; init; }
        public int BarometricPressure { get; init; }
        public decimal FuelPercentRemaining { get; init; }
        public string ModelSlug { get; init; }
        public decimal EngineLoad { get; init; }
        public string Model { get; init; }
        public string Region { get; init; }
        public string Make { get; init; }
        public DateTime Timestamp { get; init; }
        public decimal Range { get; init; }
    }
    
    #endif
}