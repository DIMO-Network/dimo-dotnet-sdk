namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class SignatureChallenge
    {
        public string State { get; set; }
        public string Challenge { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record SignatureChallenge(string State, string Challenge);

#endif
}