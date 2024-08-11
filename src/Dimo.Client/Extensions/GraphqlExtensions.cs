using Dimo.Client.Services.Identity;
using Dimo.Client.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Extensions
{
    internal static class GraphqlExtensions
    {
        public static IServiceCollection AddGraphql(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITelemetryService, TelemetryService>();
            return services;
        }
    }
}