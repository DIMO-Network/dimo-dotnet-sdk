using Dimo.Client.Graphql.Services.Identity;
using Dimo.Client.Graphql.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Graphql
{
    public static class GraphqlExtensions
    {
        public static IServiceCollection AddGraphql(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddScoped<IIdentityService>(provider => new IdentityService(environment));
            services.AddScoped<ITelemetryService>(provider => new TelemetryService(environment));
            return services;
        }
    }
}