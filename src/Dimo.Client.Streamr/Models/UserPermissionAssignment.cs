using System;

namespace Dimo.Client.Streamr.Models
{
#if NETSTANDARD
    public abstract class PermissionAssignment 
    {
        public StreamPermission[] Permissions { get; set; }
    }
    
    public class UserPermissionAssignment : PermissionAssignment
    {
        public string User { get; set; }
    }
#elif NET6_0_OR_GREATER
    public abstract record PermissionAssignment(StreamPermission[] Permissions);
    public record UserPermissionAssignment(string User) : PermissionAssignment(Array.Empty<StreamPermission>());
#endif
}