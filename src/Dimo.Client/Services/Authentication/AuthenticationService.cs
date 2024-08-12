#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
#endif
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;
using Microsoft.Extensions.Options;
using Nethereum.Signer;

namespace Dimo.Client.Services.Authentication
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ClientCredentials _credentials;
        
        public AuthenticationService(
            IHttpClientFactory httpClientFactory, 
            ClientCredentials credentials, 
            IOptions<ClientCredentials> options)
        {
            _httpClientFactory = httpClientFactory;
            _credentials = credentials ?? options.Value;
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

        public async Task<Auth> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            if (_credentials == null)
            {
                throw new ArgumentNullException(nameof(_credentials), "Client credentials are not set. Have you set them?");
            }
            
            var challenge = await GenerateChallengeAsync(_credentials.ClientId, _credentials.Domain, _credentials.Address, cancellationToken);
            var signature = await SignChallengeAsync(challenge.Challenge, _credentials.PrivateKey, cancellationToken);
            var auth = await SubmitChallengeAsync(_credentials.ClientId, _credentials.Domain, challenge.State, signature, cancellationToken);
            return auth;
        }
    }
}
