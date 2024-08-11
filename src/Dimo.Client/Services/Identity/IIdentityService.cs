using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Identity
{
    public interface IIdentityService
    {
        Task<VehicleSchemeResponse<CountResult>> CountDimoVehiclesAsync(CancellationToken cancellationToken = default);
        Task<VehicleSchemeResponse<VehicleDefinition>> ListVehiclesDefinitionsPerAddressAsync(string address, int limit, CancellationToken cancellationToken = default);
        Task<T> ExecuteQueryAsync<T>([StringSyntax("GraphQL")]string query, object variables, CancellationToken cancellationToken = default);
    }
}