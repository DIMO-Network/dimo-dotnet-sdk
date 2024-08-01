using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<SignatureChallenge> GenerateChallengeAsync(string clientId, string domain, string address, CancellationToken cancellationToken = default);
        Task<string> SignChallengeAsync(string message, string privateKey, CancellationToken cancellationToken = default);
        Task<Auth> SubmitChallengeAsync(string clientId, string domain, string state, string signature, CancellationToken cancellationToken = default);
        Task<Auth> GetTokenAsync(string clientId, string domain, string privateKey, string address, CancellationToken cancellationToken = default);
    }
}
