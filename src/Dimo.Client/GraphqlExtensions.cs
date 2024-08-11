using Dimo.Client.Services.Identity;
using Dimo.Client.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client
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