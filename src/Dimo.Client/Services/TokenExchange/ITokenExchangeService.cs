using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.TokenExchange
{
    public interface ITokenExchangeService
    {
        Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, PrivilegeSharing[] privileges, CancellationToken cancellationToken = default);
        Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, string clientId, CancellationToken cancellationToken = default);
    }
}