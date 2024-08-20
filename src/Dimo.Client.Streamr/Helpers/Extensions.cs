using System;
using System.Collections.Generic;
using System.Numerics;
using Dimo.Client.Streamr.Models;

namespace Dimo.Client.Streamr.Helpers
{
    public static class Extensions
    {
        public static bool IsPublicPermissionQuery(this PermissionQuery query)
        {
            return query is PublicPermissionQuery;
        }

        public static BigInteger ToSolidityType(this StreamPermission permission)
        {
            switch (permission)
            {
                case StreamPermission.Edit:
                    return 0;
                case StreamPermission.Delete:
                    return 1;
                case StreamPermission.Publish:
                    return 2;
                case StreamPermission.Subscribe:
                    return 3;
                case StreamPermission.Grant:
                    return 4;
                default:
                    return 0;
            }
        }

        public static IList<StreamPermission> ToStreamPermission(this ChainPermission chainPermission)
        {
            var now = DateTime.Now.Second;
            
            var permissions = new List<StreamPermission>();
            if (chainPermission.CanEdit)
            {
                permissions.Add(StreamPermission.Edit);
            }
            
            if (chainPermission.CanDelete)
            {
                permissions.Add(StreamPermission.Delete);
            }
            
            if (chainPermission.PublishExpiration > now)
            {
                permissions.Add(StreamPermission.Publish);
            }
            
            if (chainPermission.SubscribeExpiration > now)
            {
                permissions.Add(StreamPermission.Subscribe);
            }
            
            if (chainPermission.CanGrant)
            {
                permissions.Add(StreamPermission.Grant);
            }

            return permissions;
        }
        
        public static ChainPermission ToChainPermission(this IList<StreamPermission> permissions)
        {
            return new ChainPermission
            {
                CanEdit = permissions.Contains(StreamPermission.Edit),
                CanDelete = permissions.Contains(StreamPermission.Delete),
                PublishExpiration = permissions.Contains(StreamPermission.Publish) ? int.MaxValue : 0,
                SubscribeExpiration = permissions.Contains(StreamPermission.Subscribe) ? int.MaxValue : 0,
                CanGrant = permissions.Contains(StreamPermission.Grant)
            };
        }
    }
}