namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class AftermarketDevice
    {
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
    public record AftermarketDevice(string Description, string Image, string Name, AftermarketDeviceAttribute[] Attributes);

    public record AftermarketDeviceAttribute(string TraitType, string Value);
#endif
}