namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class AftermarketDevice
    {
        public long TokenId { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public AftermarketDeviceAttribute[] Attributes { get; set; }
    }

    public class AftermarketDeviceAttribute
    {
        public string TraitType { get; set; }
        public string Value { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record AftermarketDevice(long TokenId, string Address, string Description, string Image, string Name, AftermarketDeviceAttribute[] Attributes);

    public record AftermarketDeviceAttribute(string TraitType, string Value);
#endif
}