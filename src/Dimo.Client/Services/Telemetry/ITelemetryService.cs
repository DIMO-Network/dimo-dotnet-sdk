using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Dimo.Client.Services.Telemetry
{
    public interface ITelemetryService
    {
        Task<IReadOnlyCollection<Signal>> GetLatestSignalsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default);
        Task<T> ExecuteQueryAsync<T>([StringSyntax("GraphQL")]string query, object variables, string authToken, CancellationToken cancellationToken = default);
    }
}