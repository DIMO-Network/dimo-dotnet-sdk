namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class DeviceStatusConfig
    {
        public string DbcUrl { get; set; }
        public string FirmwareVersionApplied { get; set; }
        public string PidsUrl { get; set; }
        public string SettingsUrl { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceStatusConfig(string DbcUrl, string FirmwareVersionApplied, string PidsUrl, string SettingsUrl);
#endif
}