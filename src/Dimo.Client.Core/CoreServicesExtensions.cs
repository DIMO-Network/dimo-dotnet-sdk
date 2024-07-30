using System;
using Dimo.Client.Core.Services.Authentication;
using Dimo.Client.Core.Services.DeviceData;
using Dimo.Client.Core.Services.DeviceDefinitions;
using Dimo.Client.Core.Services.Devices;
using Dimo.Client.Core.Services.Events;
using Dimo.Client.Core.Services.TokenExchange;
using Dimo.Client.Core.Services.Trips;
using Dimo.Client.Core.Services.Users;
using Dimo.Client.Core.Services.Valuations;
using Dimo.Client.Core.Services.VehicleSignalDecoding;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Core
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

            services.AddTransient<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.Auth, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Auth]);
            });

            services.AddHttpClient(ApiNames.DeviceData, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.DeviceData]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.DeviceDefinitions, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.DeviceDefinitions]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.Devices, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Devices]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.TokenExchange, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.TokenExchange]);
            });
            
            services.AddHttpClient(ApiNames.Events, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Events]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.Trips, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Trips]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.User, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.User]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.Valuations, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Valuations]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            services.AddHttpClient(ApiNames.VehicleSignalDecoding, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.VehicleSignalDecoding]);
            }).AddHttpMessageHandler<AuthenticationHandler>();
            
            return services;
        }
        
        public static IServiceCollection AddCoreServices(this IServiceCollection services, DimoEnvironment environment, Action<ClientCredentials> credentials)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
    }
}