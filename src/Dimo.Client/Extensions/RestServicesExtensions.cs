using System;
using Dimo.Client.Services.Attestation;
using Dimo.Client.Services.Authentication;
using Dimo.Client.Services.DeviceDefinitions;
using Dimo.Client.Services.Devices;
using Dimo.Client.Services.TokenExchange;
using Dimo.Client.Services.Trips;
using Dimo.Client.Services.Valuations;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Extensions
{
    public static class RestServicesExtensions
    {
        internal static IServiceCollection AddDimoRestServices(this IServiceCollection services)
        {
            services.AddScoped<IAttestationService, AttestationService>();
            services.AddScoped<IDeviceDefinitionsService, DeviceDefinitionService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<ITripsService, TripsService>();
            services.AddScoped<IValuationsService, ValuationsService>();
            return services;
        }
        
        internal static IServiceCollection AddAuthServices(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenExchangeService, TokenExchangeService>();
            services.AddSingleton<RpcSigner>(Constants.RpcSigners[environment]);
            
            return services;
        }
        
        public static IServiceCollection AddDimoRestServices(this IServiceCollection services, Action<DimoClientOptions> config)
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
            
            services.AddAuthServices(clientOptions.Environment);
            services.AddDimoRestServices();
            return services;
        }
        
    }
}