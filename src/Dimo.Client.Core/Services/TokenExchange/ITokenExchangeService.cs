using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.TokenExchange
{
    public interface ITokenExchangeService
    {
        Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, int[] privileges, CancellationToken cancellationToken = default);
    }
}