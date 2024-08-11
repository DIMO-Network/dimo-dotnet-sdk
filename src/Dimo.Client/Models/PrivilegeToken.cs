namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class PrivilegeToken
    {
        public string Token { get; set; }   
    }
#elif NET6_0_OR_GREATER
    public record PrivilegeToken(string Token);
#endif
}