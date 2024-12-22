using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Attestation
{
    public interface IAttestationService
    {
        /// <summary>
        /// Creates or retrieves a Vehicle Identification Number (VIN) Verifiable Credential (VC) for a specific vehicle.
        /// If a VC has never been created or has expired, generates a new one. Otherwise, returns the existing unexpired VC.
        /// The resulting rawVC can be used for querying the Telemetry API to obtain vehicle VIN information.
        /// </summary>
        /// <param name="tokenId">The vehicle's NFT token ID. The token must have permission to access the vehicle VIN data.</param>
        /// <param name="vehicleToken">The vehicle's JWT token for authentication.</param>
        /// /// <param name="cancellationToken">Token for signalling the cancellation of the operation.</param>
        /// <returns>
        /// Returns a VinVcResponse containing:
        /// - vcUrl: The GraphQL endpoint URL for querying the VC
        /// - vcQuery: The GraphQL query template to retrieve the rawVC
        /// - message: Success confirmation message with retrieval instructions
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when tokenId or vehicleToken is null or empty.</exception>
        /// <exception cref="HttpRequestException">Thrown when the HTTP response was unsuccessful.</exception>
        /// <exception cref="Exception">
        /// Thrown when:
        /// - The request is invalid (400)
        /// - Authentication fails
        /// - The token lacks necessary permissions
        /// </exception>
        Task<VehicleVinVc> CreateVinVcAsync(long tokenId, string vehicleToken,CancellationToken cancellationToken = default);
    }
}