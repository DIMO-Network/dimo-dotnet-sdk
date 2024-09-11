namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class CreatedVehicle
    {
        public string DeviceDefinitionId { get; set; }
        public string UserDeviceId { get; set; }
        public IntegrationCapability[] IntegrationCapabilities { get; set; }
    }

    public class IntegrationCapability
    {
        public string Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Style { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; }
        public int[] Capabilities { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record CreatedVehicle(string DeviceDefinitionId, string UserDeviceId, IntegrationCapability[] IntegrationCapabilities);

    public record IntegrationCapability(string Id, string Country, string Region, string Style, string Type, string Vendor, int[] Capabilities);
#endif
}