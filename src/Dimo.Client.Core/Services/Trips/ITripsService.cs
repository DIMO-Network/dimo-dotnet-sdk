using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.Trips
{
    public interface ITripsService
    {
        Task<TripHistory> ListTripsByTokenIdAsync(string tokenId, string authToken, CancellationToken cancellationToken = default);
    }
}
