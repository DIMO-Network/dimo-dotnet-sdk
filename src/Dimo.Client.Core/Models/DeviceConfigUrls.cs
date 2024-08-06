using System;

namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class DeviceConfigUrls
    {
        public Uri DbcUrl { get; set; }
        public Uri PidUrl { get; set; }
        public Uri DeviceSettingUrl { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceConfigUrls(Uri DbcUrl, Uri PidUrl, Uri DeviceSettingUrl);
#endif
}