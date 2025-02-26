using System;
#if NETSTANDARD
using Newtonsoft.Json;
#elif  NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class VehiclesSchemeResponse<T>
    {
        public T Vehicles { get; set; }
    }
    
    public class VehicleSchemeResponse<T>
    {
        public T Vehicle { get; set; }
    }

    public class CountResult
    {
        public int TotalCount { get; set; }
    }
    
    public class VehicleDefinition
    {
        public VehicleNode[] Nodes { get; set; }
    }

    public class VehicleNode
    {
        public long TokenId { get; set; }
        public string Owner { get; set; }
        [JsonProperty("imageURI")]
        public string ImageUri { get; set; }
        public DateTimeOffset MintedAt { get; set; }
        public AftermarketDevice AftermarketDevice { get; set; }
        public AftermarketDevice SyntheticDevice { get; set; }
        public Definition Definition { get; set; }
        [JsonProperty("dcn")]
        public DimoCanonicalName DimoCanonicalName { get; set; }
        public VehicleEarnings Earnings { get; set; }
        [JsonProperty("sacds")]
        public ServiceAccessContractDefinitionConnection ServiceAccessContractDefinitions { get; set; }
        public PrivilegesConnection Privileges { get; set; }
    }

    public class Definition
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
    
    public class DimoCanonicalName
    {
        public long TokenId { get; set; }
        public string Owner { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset MintedAt { get; set; }
        public string Name { get; set; }
        public string Node { get; set; }
    }
    
    public class VehicleEarnings
    {
        public decimal TotalTokens { get; set; }
        public EarningsConnection History { get; set; }
    }
    
    public class EarningsConnection
    {
        public int TotalCount { get; set; }
        public EarningsEdge[] Edges { get; set; }
        public Earning[] Nodes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    
    public class EarningsEdge
    {
        public string Cursor { get; set; }
        public Earning Node { get; set; }
    }
    
    public class Earning
    {
        public int Week { get; set; }
        public string Beneficiary { get; set; }
        public int ConnectionStreak { get; set; }
        public decimal StreakTokens { get; set; }
        public AftermarketDevice AftermarketDevice { get; set; }
        public decimal AftermarketDeviceTokens { get; set; }
        public AftermarketDevice SyntheticDevice { get; set; }
        public decimal SyntheticDeviceTokens { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
    
    public class ServiceAccessContractDefinitionConnection
    {
        public int TotalCount { get; set; }
        public ServiceAccessContractDefinitionEdge[] Edges { get; set; }
        public ServiceAccessContractDefinition[] Nodes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    
    public class ServiceAccessContractDefinitionEdge
    {
        public string Cursor { get; set; }
        public ServiceAccessContractDefinition Node { get; set; }
    }
    
    public class ServiceAccessContractDefinition
    {
        public string Grantee { get; set; }
        public string Permissions { get; set; }
        public string Source { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
    
    public class PrivilegesConnection
    {
        public int TotalCount { get; set; }
        public PrivilegesEdge[] Edges { get; set; }
        public Privilege[] Nodes { get; set; }
        public PageInfo PageInfo { get; set; }
    }
    
    public class PrivilegesEdge
    {
        public string Cursor { get; set; }
        public Privilege Node { get; set; }
    }
    
    public class Privilege
    {
        public string User { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
    
    
#elif NET6_0_OR_GREATER
    public record VehiclesSchemeResponse<T>(T Vehicles);
    public record VehicleSchemeResponse<T>(T Vehicle);

    public record CountResult(int TotalCount);

    public record VehicleDefinition(VehicleNode[] Nodes);

    public record VehicleNode
    {
        public long TokenId { get; init; }
        public string Owner { get; init; }
        [JsonPropertyName("imageURI")]
        public string ImageUri { get; init; }
        public DateTimeOffset MintedAt { get; init; }
        public AftermarketDevice AftermarketDevice { get; init; }
        public AftermarketDevice SyntheticDevice { get; init; }
        public Definition Definition { get; init; }
        [JsonPropertyName("dcn")]
        public DimoCanonicalName DimoCanonicalName { get; init; }
        public VehicleEarnings Earnings { get; init; }
        [JsonPropertyName("sacds")]
        public ServiceAccessContractDefinitionConnection ServiceAccessContractDefinitions { get; init; }
        
        public PrivilegesConnection Privileges { get; init; }
    }

    public record DimoCanonicalName(
        long TokenId,
        string Owner,
        DateTimeOffset ExpiresAt,
        DateTimeOffset MintedAt,
        string Name,
        string Node
        );

    public record VehicleEarnings(
        decimal TotalTokens,
        EarningsConnection History
        );

    public record EarningsConnection(
        int TotalCount,
        EarningsEdge[] Edges,
        Earning[] Nodes,
        PageInfo PageInfo
        );
    
    public record EarningsEdge(
        string Cursor,
        Earning Node
        );

    public record Earning(
        int Week,
        string Beneficiary,
        int ConnectionStreak,
        decimal StreakTokens,
        AftermarketDevice AftermarketDevice,
        decimal AftermarketDeviceTokens,
        AftermarketDevice SyntheticDevice,
        decimal SyntheticDeviceTokens,
        DateTimeOffset SentAt
        );
    
    public record ServiceAccessContractDefinitionConnection
    {
        public int TotalCount { get; init; }
        public ServiceAccessContractDefinitionEdge[] Edges { get; init; }
        public ServiceAccessContractDefinition[] Nodes { get; init; }
        public PageInfo PageInfo { get; init; }
    }

    public record ServiceAccessContractDefinitionEdge
    {
        public string Cursor { get; init; }
        public ServiceAccessContractDefinition Node { get; init; }
    }

    public record ServiceAccessContractDefinition
    {
        public string Grantee { get; init; }
        public string Permissions { get; init; }
        public string Source { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset ExpiresAt { get; init; }
    }
    public record Definition(string Make, string Model, int Year);
    
    public record PrivilegesConnection
    {
        public int TotalCount { get; init; }
        public PrivilegesEdge[] Edges { get; init; }
        public Privilege[] Nodes { get; init; }
        public PageInfo PageInfo { get; init; }
    }
    
    public record PrivilegesEdge
    {
        public string Cursor { get; init; }
        public Privilege Node { get; init; }
    }
    
    public record Privilege
    {
        public string User { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset ExpiresAt { get; init; }
    }
#endif
}