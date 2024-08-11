using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;
using Nethereum.Signer;

#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif

namespace Dimo.Client.Core.Services.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public AuthenticationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<SignatureChallenge> GenerateChallengeAsync(string clientId, string domain, string address,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Auth))
            {
                var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("scope", "openid email"),
                    new KeyValuePair<string, string>("response_type", "code"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("domain", domain),
                    new KeyValuePair<string, string>("address", address)
                });
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.PostAsync($"/auth/web3/generate_challenge", content, cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SignatureChallenge>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<SignatureChallenge>(cancellationToken: cancellationToken);
#endif
            }
        }

        public Task<string> SignChallengeAsync(string message, string privateKey, CancellationToken cancellationToken = default)
        {
            var signer = new EthereumMessageSigner();
            var signedMessage = signer.EncodeUTF8AndSign(message, new EthECKey(privateKey));
            return Task.FromResult(signedMessage);
        }

        public async Task<Auth> SubmitChallengeAsync(string clientId, string domain, string state, string signature,
            CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.Auth))
            {
                var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("domain", domain),
                    new KeyValuePair<string, string>("state", state),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("signature", signature)
                });

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsync($"/auth/web3/submit_challenge", content, cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Auth>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<Auth>(cancellationToken: cancellationToken);
#endif
            }
        }

        public async Task<Auth> GetTokenAsync(string clientId, string domain, string privateKey, string address,
            CancellationToken cancellationToken = default)
        {
            var challenge = await GenerateChallengeAsync(clientId, domain, address, cancellationToken);
            var signature = await SignChallengeAsync(challenge.Challenge, privateKey, cancellationToken);
            var auth = await SubmitChallengeAsync(clientId, domain, challenge.State, signature, cancellationToken);
            return auth;
        }
    }
}
