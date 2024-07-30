namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class DeviceDefinition
    {
        
    }
#elif NET6_0_OR_GREATER
    public record DeviceDefinition();
#endif
}