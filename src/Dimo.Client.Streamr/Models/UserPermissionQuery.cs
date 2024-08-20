namespace Dimo.Client.Streamr.Models
{
#if NETSTANDARD
    public abstract class PermissionQuery
    {
        public string StreamId { get; set; }
        public StreamPermission StreamPermission { get; set; }
    }
    public class UserPermissionQuery : PermissionQuery
    {
        public string User { get; set; }
        public bool AllowPublic { get; set; }
    }
#elif NET6_0_OR_GREATER
    public abstract record PermissionQuery(string StreamId, StreamPermission StreamPermission);
    public record UserPermissionQuery(string User, bool AllowPublic) : PermissionQuery(string.Empty, StreamPermission.Subscribe);
#endif

    
}