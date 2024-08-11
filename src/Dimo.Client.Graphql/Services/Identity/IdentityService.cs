#if NETSTANDARD
using GraphQL.Client.Serializer.Newtonsoft;
#elif NET6_0_OR_GREATER
using GraphQL.Client.Serializer.SystemTextJson;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Graphql.Models;
using GraphQL;
using GraphQL.Client.Http;

namespace Dimo.Client.Graphql.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly GraphQLHttpClient _client;

        public IdentityService(DimoEnvironment environment)
        {
#if NETSTANDARD
            _client = new GraphQLHttpClient(Constants.ApiUrls[environment][ApiNames.Identity], new NewtonsoftJsonSerializer());
#elif NET6_0_OR_GREATER
            _client = new GraphQLHttpClient(Constants.ApiUrls[environment][ApiNames.Identity], new SystemTextJsonSerializer());
#endif
            _client.HttpClient.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
        }
        
        public async Task<T> ExecuteQueryAsync<T>([StringSyntax("GraphQL")]string query, object variables, CancellationToken cancellationToken = default)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };
            
            var response = await _client.SendQueryAsync<T>(graphQlRequest, cancellationToken);

            if (response.Errors == null) return response.Data;
            var errors = response.Errors.Select(e => e.Message).ToArray();
            throw new DimoGraphqlException("Something went wrong while executing query", errors);
        }

        public Task<VehicleSchemeResponse<CountResult>> CountDimoVehiclesAsync(CancellationToken cancellationToken = default)
        {
            var query = @"
                        {
                            vehicles(first: 1) {
                                totalCount
                            }
                        }
                        ";
            return ExecuteQueryAsync<VehicleSchemeResponse<CountResult>>(query, new {}, cancellationToken);
        }

        public Task<VehicleSchemeResponse<VehicleDefinition>> ListVehiclesDefinitionsPerAddressAsync(string address, int limit, CancellationToken cancellationToken = default)
        {
            var query = @"
                        {
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

            return ExecuteQueryAsync<VehicleSchemeResponse<VehicleDefinition>>(query, variables, cancellationToken);
        }

    }
}