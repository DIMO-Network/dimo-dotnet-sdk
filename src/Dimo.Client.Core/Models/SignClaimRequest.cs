namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class SignClaimRequest
    {
        public string AftermarketDeviceSignature { get; set; }
        public string UserSignature { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record SignClaimRequest(string AftermarketDeviceSignature, string UserSignature);
#endif
}