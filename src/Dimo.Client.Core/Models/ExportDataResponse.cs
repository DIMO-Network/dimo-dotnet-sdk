namespace Dimo.Client.Core.Models
{
#if NETSTANDARD
    public class ExportDataResponse
    {
        public string Status { get; set; }
        public string UserId { get; set; }
        public string UserDeviceId { get; set; }
        public string Message { get; set; }
    }
#elif NET6_0_OR_GREATER
    public record ExportDataResponse(string Status, string UserId, string UserDeviceId, string Message);
#endif
}