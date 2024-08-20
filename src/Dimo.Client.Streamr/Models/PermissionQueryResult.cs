using System.Numerics;

namespace Dimo.Client.Streamr.Models
{
    public class PermissionQueryResult : ChainPermission
    {
        public string Id { get; set; }
        public string EthereumAddress { get; set; }
    }

    public class ChainPermission
    {
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public BigInteger PublishExpiration { get; set; }
        public BigInteger SubscribeExpiration { get; set; }
        public bool CanGrant { get; set; }
    }
}