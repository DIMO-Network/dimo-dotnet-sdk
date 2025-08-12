using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Identity
{
    public interface IIdentityService
    {
        Task<VehiclesSchemeResponse<CountResult>> CountDimoVehiclesAsync(CancellationToken cancellationToken = default);
        Task<VehiclesSchemeResponse<VehicleDefinition>> ListVehiclesDefinitionsPerAddressAsync(string address, int limit, CancellationToken cancellationToken = default);
        Task<VehicleSchemeResponse<VehicleNode>> ListTokenIdsPrivilegesByOwnerAsync(string address, int vehicleLimit, int privilegesLimit, CancellationToken cancellationToken = default);
        Task<VehiclesSchemeResponse<VehicleDefinition>> ListTokenIdsGrantedToDevByOwnerAsync(string ownerAddress, string devAddress, int vehicleLimit, int privilegesLimit, CancellationToken cancellationToken = default);
        Task<VehiclesSchemeResponse<VehicleNode>> GetMakeModelYearByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default);
        Task<VehicleSchemeResponse<VehicleNode>> GetMakeModelYearByTokenIdAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<VehiclesSchemeResponse<VehicleDefinition>> GetDimoCanonicalNameByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default);
        Task<UserReward> GetRewardsByOwnerAsync(string address, CancellationToken cancellationToken = default);
        Task<VehiclesSchemeResponse<VehicleDefinition>> GetRewardsHistoryByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default);
        Task<VehicleSchemeResponse<VehicleNode>> CheckVehiclePrivilegesAsync(long tokenId, CancellationToken cancellationToken = default);
        Task<T> ExecuteQueryAsync<T>([StringSyntax("GraphQL")]string query, object variables, string queryName, CancellationToken cancellationToken = default);
    }
}