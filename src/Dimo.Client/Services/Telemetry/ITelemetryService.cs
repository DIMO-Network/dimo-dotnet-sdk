using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Telemetry
{
    public interface ITelemetryService
    {
        Task<LatestSignal> GetLatestSignalsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<T> ExecuteQueryAsync<T>(string authToken, [StringSyntax("GraphQL")]string query, object variables, string queryName = "", CancellationToken cancellationToken = default);
    }
}