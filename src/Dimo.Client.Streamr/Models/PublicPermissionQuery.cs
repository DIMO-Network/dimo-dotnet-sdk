namespace Dimo.Client.Streamr.Models
{
#if NETSTANDARD
    public class PublicPermissionQuery : PermissionQuery
    {
    }
#elif NET6_0_OR_GREATER
    public record PublicPermissionQuery() : PermissionQuery(string.Empty, StreamPermission.Subscribe);
#endif
}