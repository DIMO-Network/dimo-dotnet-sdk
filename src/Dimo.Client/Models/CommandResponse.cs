namespace Dimo.Client.Models
{
#if NETSTANDARD
    public class CommandResponse
    {
        public string RequestId { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record CommandResponse(string RequestId);
#endif
}