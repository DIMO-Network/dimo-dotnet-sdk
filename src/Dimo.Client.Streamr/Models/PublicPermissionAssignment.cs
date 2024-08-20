using System;

namespace Dimo.Client.Streamr.Models
{
#if NETSTANDARD
    public class PublicPermissionAssignment : PermissionAssignment
    {
        
    }
#elif NET6_0_OR_GREATER
    public record PublicPermissionAssignment() : PermissionAssignment(Array.Empty<StreamPermission>());
#endif
}