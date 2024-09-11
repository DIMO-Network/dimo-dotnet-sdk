using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Valuations
{
    public interface IValuationsService
    {
        Task<IReadOnlyCollection<Valuation>> GetValuationsAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default);
        Task GetInstantOfferAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<OfferData>> GetOffersAsync(string accessToken, long tokenId, CancellationToken cancellationToken = default);
    }
}
