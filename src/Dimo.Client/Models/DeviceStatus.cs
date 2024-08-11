namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class DeviceStatus
    {
        public bool IsTemplateUpToDate { get; set; }
        public string FirmwareVersion { get; set; }
        public bool IsFirmwareUpToDate { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceStatus(bool IsTemplateUpToDate, string FirmwareVersion, bool IsFirmwareUpToDate);
#endif
}