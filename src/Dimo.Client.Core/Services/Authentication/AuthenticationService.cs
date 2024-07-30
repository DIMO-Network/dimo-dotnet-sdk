using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        public Task<SignatureChallenge> GenerateChallengeAsync(string clientId, string domain, string address,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> SignChallengeAsync(string message, string privateKey, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Auth> SubmitChallengeAsync(string clientId, string domain, string state, string signature,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task GetTokenAsync(string clientId, string domain, string privateKey, string address,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
