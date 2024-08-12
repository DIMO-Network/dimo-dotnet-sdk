using System;
using Dimo.Client.Services.Authentication;
using Dimo.Client.Services.DeviceData;
using Dimo.Client.Services.DeviceDefinitions;
using Dimo.Client.Services.Devices;
using Dimo.Client.Services.Events;
using Dimo.Client.Services.TokenExchange;
using Dimo.Client.Services.Trips;
using Dimo.Client.Services.Users;
using Dimo.Client.Services.Valuations;
using Dimo.Client.Services.VehicleSignalDecoding;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Extensions
{
    public static class RestServicesExtensions
    {
        internal static IServiceCollection AddDimoRestServices(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenExchangeService, TokenExchangeService>();
            services.AddScoped<IDeviceDataService, DeviceDataService>();
            services.AddScoped<IDeviceDefinitionsService, DeviceDefinitionService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ITripsService, TripsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IValuationsService, ValuationsService>();
            services.AddScoped<IVehicleSignalDecodingService, VehicleSignalDecodingService>();
            
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
            
            services.AddDimoRestServices(clientOptions.Environment);
            return services;
        }
        
    }
}