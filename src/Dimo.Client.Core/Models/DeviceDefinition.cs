using System;
using System.Collections.Generic;
#if NETSTANDARD
using Newtonsoft.Json;
#elif NET6_0_OR_GREATER
using System.Text.Json.Serialization;
#endif

namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class DeviceDefinition
    {
        [JsonProperty("deviceDefinitionId")]
        public string DeviceDefinitionId { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("hardware_template_id")]
        public string HardwareTemplateId { get; set; }

        [JsonProperty("make")]
        public Make Make { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }

        [JsonProperty("vehicleData")]
        public VehicleData VehicleData { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("externalIds")]
        public List<ExternalId> ExternalIds { get; set; }

        [JsonProperty("deviceIntegrations")]
        public List<DeviceIntegration> DeviceIntegrations { get; set; }

        [JsonProperty("compatibleIntegrations")]
        public List<CompatibleIntegration> CompatibleIntegrations { get; set; }

        [JsonProperty("deviceStyles")]
        public List<object> DeviceStyles { get; set; }

        [JsonProperty("deviceAttributes")]
        public List<DeviceAttribute> DeviceAttributes { get; set; }
    }
    
    public class CompatibleIntegration
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }

    public class DeviceAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("options")]
        public object Options { get; set; }
    }

    public class DeviceIntegration
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }

    public class ExternalId
    {
        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ExternalIds
    {
        [JsonProperty("edmunds")]
        public string Edmunds { get; set; }

        [JsonProperty("parkers")]
        public string Parkers { get; set; }
    }

    public class ExternalIdsTyped
    {
        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Feature
    {
        [JsonProperty("featureKey")]
        public string FeatureKey { get; set; }

        [JsonProperty("supportLevel")]
        public int SupportLevel { get; set; }
    }

    public class Make
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo_url")]
        public object LogoUrl { get; set; }

        [JsonProperty("oem_platform_name")]
        public string OemPlatformName { get; set; }

        [JsonProperty("tokenId")]
        public int TokenId { get; set; }

        [JsonProperty("nameSlug")]
        public string NameSlug { get; set; }

        [JsonProperty("external_ids")]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("externalIdsTyped")]
        public List<ExternalIdsTyped> ExternalIdsTyped { get; set; }

        [JsonProperty("metadata")]
        public object Metadata { get; set; }

        [JsonProperty("metadataTyped")]
        public object MetadataTyped { get; set; }

        [JsonProperty("hardware_template_id")]
        public object HardwareTemplateId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
    
    public class Type
    {
        [JsonProperty("type")]
        public string TypeName { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("subModels")]
        public object SubModels { get; set; }

        [JsonProperty("makeSlug")]
        public string MakeSlug { get; set; }

        [JsonProperty("modelSlug")]
        public string ModelSlug { get; set; }
    }

    public class VehicleData
    {
    }
#elif NET6_0_OR_GREATER
    public record DeviceDefinition
    {
        [JsonPropertyName("deviceDefinitionId")]
        public string DeviceDefinitionId { get; init; }
        
        [JsonPropertyName("external_id")]
        public string ExternalId{ get; init; }
        
        [JsonPropertyName("name")]
        public string Name{ get; init; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl{ get; init; }
        
        [JsonPropertyName("source")]
        public string Source{ get; init; }
        
        [JsonPropertyName("hardware_template_id")]
        public string HardwareTemplateId{ get; init; }
        
        [JsonPropertyName("make")]
        public Make Make{ get; init; }
        
        [JsonPropertyName("type")]
        public Type Type{ get; init; }
        
        [JsonPropertyName("vehicleData")]
        public Dictionary<string,string> VehicleData{ get; init; }
        
        [JsonPropertyName("metadata")]
        public string Metadata{ get; init; }
        
        [JsonPropertyName("verified")]
        public bool Verified{ get; init; }
        
        [JsonPropertyName("externalIds")]
        public List<ExternalId> ExternalIds{ get; init; }
        
        [JsonPropertyName("deviceIntegrations")]
        public List<DeviceIntegration> DeviceIntegrations{ get; init; }
        
        [JsonPropertyName("compatibleIntegrations")]
        public List<CompatibleIntegration> CompatibleIntegrations{ get; init; }
        
        [JsonPropertyName("deviceStyles")]
        public List<object> DeviceStyles{ get; init; }
        
        [JsonPropertyName("deviceAttributes")]
        public List<DeviceAttribute> DeviceAttributes{ get; init; }
    }

    public record CompatibleIntegration
    {
        [JsonPropertyName("id")]
        public string Id{ get; init; }
        
        [JsonPropertyName("type")]
        public string Type{ get; init; }
        
        [JsonPropertyName("style")]
        public string Style{ get; init; }
        
        [JsonPropertyName("vendor")]
        public string Vendor{ get; init; }
        
        [JsonPropertyName("region")]
        public string Region{ get; init; }
        
        [JsonPropertyName("features")]
        public List<Feature> Features{ get; init; }
    }

    public record DeviceAttribute
    {
        [JsonPropertyName("name")]
        public string Name {get; init;}
        
        [JsonPropertyName("label")]
        public string Label {get; init;}
        
        [JsonPropertyName("description")]
        public string Description {get; init;}
        
        [JsonPropertyName("type")]
        public string Type {get; init;}
        
        [JsonPropertyName("required")]
        public bool Required {get; init;}
        
        [JsonPropertyName("value")]
        public string Value {get; init;}
        
        [JsonPropertyName("options")]
        public object Options {get; init;}
    }

    public record DeviceIntegration
    {
        [JsonPropertyName("id")]
        public string Id {get; init; }
        
        [JsonPropertyName("type")]
        public string Type {get; init; }
        
        [JsonPropertyName("style")]
        public string Style {get; init; }
        
        [JsonPropertyName("vendor")]
        public string Vendor {get; init; }
        
        [JsonPropertyName("region")]
        public string Region {get; init; }
        
        [JsonPropertyName("features")]
        public List<Feature> Features {get; init; }
    }

    public record ExternalId
    {
        [JsonPropertyName("vendor")]
        public string Vendor {get; init; }
        
        [JsonPropertyName("id")]
        public string Id {get; init; }
    }

    public record ExternalIds
    {
        [JsonPropertyName("edmunds")]
        public string Edmunds {get; init; }
        
        [JsonPropertyName("parkers")]
        public string Parkers {get; init; }
    }

    public record ExternalIdsTyped
    {
        [JsonPropertyName("vendor")]
        public string Vendor {get; init; }
        
        [JsonPropertyName("id")]
        public string Id {get; init; }
    }

    public record Feature
    {
        [JsonPropertyName("featureKey")]
        public string FeatureKey {get; init; }
        
        [JsonPropertyName("supportLevel")]
        public int SupportLevel {get; init; }
    }

    public record Make
    {
        [JsonPropertyName("id")]
        public string Id {get; init; }
        
        [JsonPropertyName("name")]
        public string Name {get; init; }
        
        [JsonPropertyName("logo_url")]
        public object LogoUrl {get; init; }
        
        [JsonPropertyName("oem_platform_name")]
        public string OemPlatformName {get; init; }
        
        [JsonPropertyName("tokenId")]
        public int TokenId {get; init; }
        
        [JsonPropertyName("nameSlug")]
        public string NameSlug {get; init; }
        
        [JsonPropertyName("external_ids")]
        public ExternalIds ExternalIds {get; init; }
        
        [JsonPropertyName("externalIdsTyped")]
        public List<ExternalIdsTyped> ExternalIdsTyped {get; init; }
        
        [JsonPropertyName("metadata")]
        public object Metadata {get; init; }
        
        [JsonPropertyName("metadataTyped")]
        public object MetadataTyped {get; init; }
        
        [JsonPropertyName("hardware_template_id")]
        public object HardwareTemplateId {get; init; }
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt {get; init; }
        
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt {get; init; }
    }

    public record Type
    {
        [JsonPropertyName("type")]
        public string TypeName {get; init; }
        
        [JsonPropertyName("make")]
        public string Make {get; init; }
        
        [JsonPropertyName("model")]
        public string Model {get; init; }
        
        [JsonPropertyName("year")]
        public int Year {get; init; }
        
        [JsonPropertyName("subModels")]
        public object SubModels {get; init; }
        
        [JsonPropertyName("makeSlug")]
        public string MakeSlug {get; init; }
        
        [JsonPropertyName("modelSlug")]
        public string ModelSlug {get; init; }
    }
#endif
}