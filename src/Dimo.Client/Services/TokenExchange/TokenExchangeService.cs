#if NETSTANDARD
using Newtonsoft.Json;                    
#elif NET6_0_OR_GREATER
using System.Net.Http.Json;
using System.Text.Json;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Exceptions;
using Dimo.Client.Models;
using Dimo.Client.Services.Identity;
using Microsoft.Extensions.Options;

namespace Dimo.Client.Services.TokenExchange
{
    internal sealed class TokenExchangeService : ITokenExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        private readonly RpcSigner _rpcSigner;
        private readonly ClientCredentials _credentials;
        
        public TokenExchangeService(
            IHttpClientFactory httpClientFactory, 
            IIdentityService identityService, 
            IOptions<ClientCredentials> options,
            RpcSigner rpcSigner)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
            _rpcSigner = rpcSigner;
            _credentials =  options.Value;
        }

        public async Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, PrivilegeSharing[] privileges, CancellationToken cancellationToken = default)
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

        public async Task<PrivilegeToken> GetPrivilegeTokenAsync(string accessToken, long tokenId, string clientId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(clientId)) 
                clientId = _credentials.ClientId;
            
            if (string.IsNullOrWhiteSpace(clientId))
                throw new DimoException("Client ID must be provided. Please make sure you've obtained a Developer JWT before calling token exchange");
            
            var privileges = await DecodeVehiclePermissionsAsync(tokenId, clientId, cancellationToken);
            return await GetPrivilegeTokenAsync(accessToken, tokenId, privileges.ToArray(), cancellationToken);
        }

        private async Task<IList<PrivilegeSharing>> DecodeVehiclePermissionsAsync(long tokenId, string clientId, CancellationToken cancellationToken = default)
        {
            var privileges = await _identityService.CheckVehiclePrivilegesAsync(tokenId, cancellationToken);
            if (privileges.Vehicle == null)
            {
                throw new DimoException("Vehicle not found");
            }

            var foundSacd =
                privileges.Vehicle.ServiceAccessContractDefinitions.Nodes.FirstOrDefault(node =>
                    node.Grantee.Equals(clientId));
            
            if (foundSacd == null)
            {
                throw new DimoException("Client does not have access to this vehicle");
            }
            
            return DecodePermissionBits(foundSacd.Permissions);
        }
        
        private IList<PrivilegeSharing> DecodePermissionBits(string permissionHex)
        {
            if (string.IsNullOrWhiteSpace(permissionHex)) throw new ArgumentNullException(nameof(permissionHex), "Permission hex must not be null or empty");
            var cleanHex = permissionHex.ToLower().Replace("0x", "");
            var permissionBits = Convert.ToUInt64(cleanHex, fromBase: 16);
            var permissions = new List<PrivilegeSharing>();

            for (var i = 0; i < 128; i++)
            {
                var bitPair = (int)((permissionBits >> i) & 0b11);
                if (bitPair == 0b11) 
                    permissions.Add((PrivilegeSharing)i);
            }
            
            return permissions;
        }
    }
}