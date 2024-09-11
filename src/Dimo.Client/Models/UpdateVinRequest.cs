namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class UpdateVinRequest
    {
        public string Vin { get; set; }
        public string Signature { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record UpdateVinRequest(string Vin, string Signature);
#endif
}