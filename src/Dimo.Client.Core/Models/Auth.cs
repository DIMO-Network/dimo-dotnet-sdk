namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class Auth
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string IdToken { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record Auth
    {
        public string AccessToken { get; init; }
        public string TokenType { get; init; }
        public int ExpiresIn { get; init; }
        public string IdToken { get; init; }
    }
#endif
}