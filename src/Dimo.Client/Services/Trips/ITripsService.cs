using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Trips
{
    public interface ITripsService
    {
        Task<TripHistory> ListTripsByTokenIdAsync(string tokenId, string authToken, CancellationToken cancellationToken = default);
    }
}
