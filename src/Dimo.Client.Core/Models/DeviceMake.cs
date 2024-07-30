namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class DeviceMake
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OemPlatformName { get; set; }
        public long TokenId { get; set; }
        public string NameSlug { get; set; }
        public string ExternalIds { get; set; }
        public string Metadata { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record DeviceMake(string Id, string Name, string OemPlatformName, long TokenId, string NameSlug, string ExternalIds, string Metadata);
#endif
}