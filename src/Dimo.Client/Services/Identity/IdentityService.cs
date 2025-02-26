#if NETSTANDARD
using GraphQL.Client.Serializer.Newtonsoft;
#elif NET6_0_OR_GREATER
using GraphQL.Client.Serializer.SystemTextJson;
#endif
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Exceptions;
using Dimo.Client.Models;
using GraphQL;
using GraphQL.Client.Http;

namespace Dimo.Client.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly GraphQLHttpClient _client;

        public IdentityService(IHttpClientFactory factory)
        {
            var httpClient = factory.CreateClient(ApiNames.Identity);
#if NETSTANDARD
            _client = new GraphQLHttpClient(httpClient.BaseAddress, new NewtonsoftJsonSerializer(), httpClient);
#elif NET6_0_OR_GREATER
            _client = new GraphQLHttpClient(httpClient.BaseAddress!, new SystemTextJsonSerializer(), httpClient);
#endif
        }

        public Task<VehicleSchemeResponse<VehicleNode>> CheckVehiclePrivilegesAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            const string query = @"query CheckPrivileges($tokenId: Int!) {
                                          vehicle(tokenId: $tokenId) {
                                            sacds(first: 100) {
                                              nodes {
                                                permissions
                                                grantee
                                              }                                              
                                            }
                                          }
                                        }";
            var variables = new
            {
                tokenId
            };
            return ExecuteQueryAsync<VehicleSchemeResponse<VehicleNode>>(
                query, 
                variables,
                queryName: "CheckPrivileges", 
                cancellationToken: cancellationToken);
        }

        public async Task<T> ExecuteQueryAsync<T>(
            [StringSyntax("GraphQL")]string query, 
            object variables, 
            string queryName = "", 
            CancellationToken cancellationToken = default)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = query,
                OperationName = queryName,
                Variables = variables
            };
            
            var response = await _client.SendQueryAsync<T>(graphQlRequest, cancellationToken);
            
            if (response.Errors == null) return response.Data;
            var errors = response.Errors.Select(e => e.Message).ToArray();
            throw new DimoGraphqlException("Something went wrong while executing query", errors);
        }

        public Task<VehiclesSchemeResponse<CountResult>> CountDimoVehiclesAsync(CancellationToken cancellationToken = default)
        {
            const string query = @"
                        {
                            vehicles(first: 1) {
                                totalCount
                            }
                        }
                        ";
            return ExecuteQueryAsync<VehiclesSchemeResponse<CountResult>>(query, new {}, cancellationToken: cancellationToken);
        }

        public Task<VehiclesSchemeResponse<VehicleDefinition>> ListVehiclesDefinitionsPerAddressAsync(string address, int limit, CancellationToken cancellationToken = default)
        {
            const string query = @"
                        query ListVehiclesDefinitionsPerAddress($address: Address!, $limit: Int!) {
                            vehicles(filterBy: {owner: $address}, first: $limit) {
                              nodes {
                                aftermarketDevice {
                                    tokenId
                                    address
                                }
                                  syntheticDevice {
                                    address
                                    tokenId
                                }
                                definition {
                                  make
                                  model
                                  year
                                }
                              }
                            }
                        }
                        ";
            
            var variables = new
            {
                address,
                limit
            };
            
            return ExecuteQueryAsync<VehiclesSchemeResponse<VehicleDefinition>>(
                query, 
                variables, 
                queryName: "ListVehiclesDefinitionsPerAddress", 
                cancellationToken: cancellationToken);
        }

        public Task<VehicleSchemeResponse<VehicleNode>> ListTokenIdsPrivilegesByOwnerAsync(string address, int vehicleLimit, int privilegesLimit,
            CancellationToken cancellationToken = default)
        {
            const string query = @"
                                query TokenIDsPrivilegesByOwner($owner: Address!, $firstVehicles: Int!, $firstPrivileges: Int!) {
                                      vehicles(filterBy: {owner: $owner}, first: $firstVehicles) {
                                        nodes {
                                          tokenId
                                          privileges(first: $firstPrivileges) {
                                            nodes {
                                              setAt
                                              expiresAt
                                              id
                                            }
                                          }
                                        }
                                      }
                                    }";

            var variables = new
            {
                owner = address,
                firstVehicles = vehicleLimit,
                firstPrivileges = privilegesLimit
            };
            
            return ExecuteQueryAsync<VehicleSchemeResponse<VehicleNode>>(
                query, 
                variables, 
                queryName: "TokenIDsPrivilegesByOwner", 
                cancellationToken: cancellationToken);
        }

        public Task<VehiclesSchemeResponse<VehicleDefinition>> ListTokenIdsGrantedToDevByOwnerAsync(string ownerAddress, string devAddress, int vehicleLimit, int privilegesLimit,
            CancellationToken cancellationToken = default)
        {
            const string query =
                @"query ListTokenIdsGrantedToDevByOwner($privileged: Address!, $owner: Address!, $first: Int!) {
                      vehicles(filterBy: {privileged: $privileged, owner: $owner}, first: $first) {
                        nodes {
                          tokenId
                          definition {
                            make
                          }
                          aftermarketDevice {
                            manufacturer {
                              name
                            }
                          }
                        }
                      }
                    }";
            
            var variables = new
            {
                privileged = devAddress,
                owner = ownerAddress,
                first = vehicleLimit
            };
            
            return ExecuteQueryAsync<VehiclesSchemeResponse<VehicleDefinition>>(
                query, 
                variables, 
                queryName: "ListTokenIdsGrantedToDevByOwner", 
                cancellationToken: cancellationToken);
        }

        public Task<VehiclesSchemeResponse<VehicleNode>> GetMakeModelYearByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default)
        {
            const string query = @"query MMYByOwner($owner: Address!, $first: Int!) {
                                          vehicles(filterBy: {owner: $owner}, first: $first) {
                                            nodes {
                                              definition {
                                                make
                                                model
                                                year
                                              }
                                            }
                                          }
                                        }";
            var variables = new
            {
                owner = address,
                first = limit
            };
            
            return ExecuteQueryAsync<VehiclesSchemeResponse<VehicleNode>>(
                query, 
                variables, 
                queryName: "MMYByOwner", 
                cancellationToken: cancellationToken);
        }

        public Task<VehicleSchemeResponse<VehicleNode>> GetMakeModelYearByTokenIdAsync(long tokenId, CancellationToken cancellationToken = default)
        {
            const string query = @"query MMYByTokenID($tokenId: Int!) {
                                        vehicle (tokenId: $tokenId) {
                                          aftermarketDevice {
                                            tokenId
                                            address
                                          }
                                          syntheticDevice {
                                            tokenId
                                            address
                                          }
                                          definition {
                                            make
                                            model
                                            year
                                          }
                                        }
                                      }";
            var variables = new
            {
                tokenId
            };
            
            return ExecuteQueryAsync<VehicleSchemeResponse<VehicleNode>>(
                query, 
                variables, 
                queryName: "MMYByTokenID", 
                cancellationToken: cancellationToken);
        }

        public Task<VehiclesSchemeResponse<VehicleDefinition>> GetDimoCanonicalNameByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default)
        {
            const string query = @"query DCNByOwner($owner: Address!, $first: Int!) {
                                      vehicles(filterBy: {owner: $owner}, first: $first) {
                                        nodes {
                                          dcn {
                                            node
                                            name
                                            vehicle {
                                              tokenId
                                            }
                                          }
                                        }
                                      }
                                    }";
            var variables = new
            {
                owner = address,
                first = limit
            };
            
            return ExecuteQueryAsync<VehiclesSchemeResponse<VehicleDefinition>>(
                query, 
                variables, 
                queryName: "DCNByOwner", 
                cancellationToken: cancellationToken);
        }

        public Task<UserReward> GetRewardsByOwnerAsync(string address, CancellationToken cancellationToken = default)
        {
            const string query = @"query RewardsByOwner($owner: Address!) {
                                        rewards (user: $owner) {
                                          totalTokens
                                        }
                                      }";
            var variables = new
            {
                owner = address
            };
            
            return ExecuteQueryAsync<UserReward>(
                query, 
                variables, 
                queryName: "RewardsByOwner", 
                cancellationToken: cancellationToken);
        }

        public Task<VehiclesSchemeResponse<VehicleDefinition>> GetRewardsHistoryByOwnerAsync(string address, int limit, CancellationToken cancellationToken = default)
        {
            const string query = @"
                                query GetVehicleDataByOwner($owner: Address!, $first: Int!) {
                                        vehicles (filterBy: {owner: $owner}, first: $first) {
                                          nodes {
                                            earnings {
                                              history (first: $first) {
                                                edges {
                                                  node {
                                                    week
                                                    aftermarketDeviceTokens
                                                    syntheticDeviceTokens
                                                    sentAt
                                                    beneficiary
                                                    connectionStreak
                                                    streakTokens
                                                  }
                                                }
                                              }
                                              totalTokens
                                            }
                                          }
                                        }
                                      }";

            var variables = new
            {
                owner = address,
                first = limit
            };
            
            return ExecuteQueryAsync<VehiclesSchemeResponse<VehicleDefinition>>(
                query, 
                variables, 
                queryName: "GetVehicleDataByOwner", 
                cancellationToken: cancellationToken);
        }
    }
}