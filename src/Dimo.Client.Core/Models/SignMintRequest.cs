namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class SignMintRequest
    {
        public string ImageData { get; set; }
        public string ImageDataTransparent { get; set; }
        public string Signature { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record SignMintRequest(string ImageData, string ImageDataTransparent, string Signature);
#endif
}