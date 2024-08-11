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

namespace Dimo.Client
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDeviceDataService, DeviceDataService>();
            services.AddScoped<IDeviceDefinitionsService, DeviceDefinitionService>();
            services.AddScoped<IDevicesService, DevicesService>();
            services.AddScoped<ITokenExchangeService, TokenExchangeService>();
            services.AddScoped<IEventsService, EventsService>();
            services.AddScoped<ITripsService, TripsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IValuationsService, ValuationsService>();
            services.AddScoped<IVehicleSignalDecodingService, VehicleSignalDecodingService>();
            
            services.AddSingleton<RpcSigner>(Constants.RpcSigners[environment]);
            
            foreach (var apis in Constants.ApiUrls[environment])
            {
                services.AddHttpClient(apis.Key, client =>
                {
                    client.BaseAddress = new Uri(apis.Value);
                    client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
                });
            }
            
            return services;
        }
        
        public static IServiceCollection AddCoreServices(this IServiceCollection services, DimoEnvironment environment, Action<ClientCredentials> credentials)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
    }
}