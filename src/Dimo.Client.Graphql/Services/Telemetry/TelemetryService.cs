#if NETSTANDARD
using GraphQL.Client.Serializer.Newtonsoft;
#elif NET6_0_OR_GREATER
using GraphQL.Client.Serializer.SystemTextJson;
#endif
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Graphql.Models;
using GraphQL;
using GraphQL.Client.Http;

namespace Dimo.Client.Graphql.Services.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        private readonly GraphQLHttpClient _client;

        public TelemetryService(DimoEnvironment environment)
        {
#if NETSTANDARD
            _client = new GraphQLHttpClient(Constants.ApiUrls[environment][ApiNames.Telemetry], new NewtonsoftJsonSerializer());
#elif NET6_0_OR_GREATER
            _client = new GraphQLHttpClient(Constants.ApiUrls[environment][ApiNames.Telemetry], new SystemTextJsonSerializer());
#endif
            _client.HttpClient.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
        }
        
        public Task<IReadOnlyCollection<Signal>> GetLatestSignalsAsync(long tokenId, string authToken, CancellationToken cancellationToken = default)
        {
            const string query = @"
                                 {
                                    signalsLatest(tokenID: $tokenId){
                                     powertrainTransmissionTravelledDistance {
                                         timestamp
                                         value
                                     }
                                     exteriorAirTemperature {
                                         timestamp
                                         value
                                     }
                                     speed{
                                         timestamp
                                         value
                                     }
                                     powertrainType{
                                         timestamp
                                         value
                                     }
                                    }
                                 }
                                 ";
            
            var variables = new
            {
                tokenId
            };
            
            return ExecuteQueryAsync<IReadOnlyCollection<Signal>>(query, variables, authToken, cancellationToken);
        }

        public async Task<T> ExecuteQueryAsync<T>([StringSyntax("GraphQL")]string query, object variables, string authToken,
            CancellationToken cancellationToken = default)
        {
            var graphQlRequest = new GraphQLRequest
            {
                Query = query,
                Variables = variables
            };
            _client.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");
            
            var response = await _client.SendQueryAsync<T>(graphQlRequest, cancellationToken);

            if (response.Errors == null) return response.Data;
            
            var errors = response.Errors.Select(e => e.Message).ToArray();
            throw new DimoGraphqlException("Something went wrong while executing query", errors);
        }
    }
}