namespace Dimo.Client.Models
{
    
#if NETSTANDARD
    public class VehicleVinVc
    {
        public string VcUrl { get; set; }
        public string VcQuery { get; set; }
        public string Message { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record VehicleVinVc
    {
        public string VcUrl { get; init; }
        public string VcQuery { get; init; }
        public string Message { get; init; }
    }
#endif
}