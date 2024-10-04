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

        public Task<VehicleSchemeResponse<CountResult>> CountDimoVehiclesAsync(CancellationToken cancellationToken = default)
        {
            const string query = @"
                        {
                            vehicles(first: 1) {
                                totalCount
                            }
                        }
                        ";
            return ExecuteQueryAsync<VehicleSchemeResponse<CountResult>>(query, new {}, cancellationToken: cancellationToken);
        }

        public Task<VehicleSchemeResponse<VehicleDefinition>> ListVehiclesDefinitionsPerAddressAsync(string address, int limit, CancellationToken cancellationToken = default)
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

            
            return ExecuteQueryAsync<VehicleSchemeResponse<VehicleDefinition>>(
                query, 
                variables, 
                queryName: "ListVehiclesDefinitionsPerAddress", 
                cancellationToken: cancellationToken);
        }

    }
}