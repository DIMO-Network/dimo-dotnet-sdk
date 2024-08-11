#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif
using System.Net.Http;
using System.Text;

using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.TokenExchange
{
    internal sealed class TokenExchangeService : ITokenExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RpcSigner _rpcSigner;
        public TokenExchangeService(IHttpClientFactory httpClientFactory, RpcSigner rpcSigner)
        {
            _httpClientFactory = httpClientFactory;
            _rpcSigner = rpcSigner;
        }

        public async Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, int[] privileges, CancellationToken cancellationToken = default)
        {
            using (var client = _httpClientFactory.CreateClient(ApiNames.TokenExchange))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
#if NETSTANDARD
                var content = new StringContent(JsonConvert.SerializeObject(new
                {
                    nftContractAddress =  _rpcSigner.NftAddress,
                    tokenId, 
                    privileges
                }), Encoding.UTF8, "application/json");
#elif NET6_0_OR_GREATER
                var content = new StringContent(JsonSerializer.Serialize(new
                {
                    nftContractAddress =  _rpcSigner.NftAddress, 
                    tokenId, 
                    privileges
                }), Encoding.UTF8, "application/json");
#endif
                var response = await client.PostAsync($"/v1/tokens/exchange", content, cancellationToken);

                response.EnsureSuccessStatusCode();
#if NETSTANDARD
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PrivilegeToken>(json);
#elif NET6_0_OR_GREATER
                return await response.Content.ReadFromJsonAsync<PrivilegeToken>(cancellationToken: cancellationToken); 
#endif
                
            }
        }
    }
}