using System;
using Dimo.Client.Services.Identity;
using Dimo.Client.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Extensions
{
    public static class GraphqlServicesExtensions
    {
        internal static IServiceCollection AddDimoGraphQlServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITelemetryService, TelemetryService>();
            return services;
        }
        
        public static IServiceCollection AddDimoGraphQlServices(this IServiceCollection services, Action<DimoClientOptions> config)
        {
            var clientOptions = new DimoClientOptions();
            config(clientOptions);
            
            foreach (var apis in Constants.ApiUrls[clientOptions.Environment])
            {
                services.AddHttpClient(apis.Key, client =>
                {
                    client.BaseAddress = new Uri(apis.Value);
                    client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
                });
            }
            
            services.AddDimoGraphQlServices();
            
            // This to add Auth related services cause some graphql services require authentication 
            services.AddAuthServices(clientOptions.Environment);
            
            return services;
        }
    }
}