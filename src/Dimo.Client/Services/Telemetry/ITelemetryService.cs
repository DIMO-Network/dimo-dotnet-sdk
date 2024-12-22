using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Exceptions;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Telemetry
{
    public interface ITelemetryService
    {
        /// <summary>
        /// Retrieves the latest Vehicle Identification Number (VIN) Verifiable Credential (VC) for a specific vehicle.
        /// This method should be called after successfully calling CreateVinVcAsync to get the actual VC data.
        /// </summary>
        /// <param name="tokenId">The vehicle's NFT token ID as a long integer</param>
        /// <param name="vehicleToken">The vehicle's JWT token for authentication</param>
        /// <param name="cancellationToken">Optional cancellation token to cancel the operation</param>
        /// <exception cref="ArgumentException">
        ///     Thrown when:
        ///     - tokenId is invalid
        ///     - vehicleToken is null or empty
        /// </exception>
        /// <exception cref="DimoGraphqlException">
        ///     Thrown when:
        ///     - The API request fails
        ///     - Authentication fails
        ///     - The token lacks necessary permissions
        ///     - No VC exists for the given token ID
        /// </exception>
        /// <remarks>
        /// Typical workflow:
        /// 1. First call CreateVinVcAsync to ensure a VC exists
        /// 2. Then call this method to retrieve the actual VC data
        /// 
        /// Example usage:
        /// var vc = await client.CreateVinVcAsync(tokenId, vehicleToken);
        /// var latestVc = await client.GetVinVcLatestAsync(tokenId, vehicleToken);
        /// </remarks>
        
        Task<VehicleVinVcLatest> GetVinVcLatestAsync(long tokenId,string vehicleToken, CancellationToken cancellationToken = default);
        Task<LatestSignal> GetLatestSignalsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<T> ExecuteQueryAsync<T>(string authToken, [StringSyntax("GraphQL")]string query, object variables, string queryName = "", CancellationToken cancellationToken = default);
    }
}