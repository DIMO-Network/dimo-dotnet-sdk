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
            
            services.AddSingleton<RpcSigner>(Constants.RpcSigners[environment]);
            
            services.AddHttpClient(ApiNames.Auth, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Auth]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.TokenExchange, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.TokenExchange]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });

            services.AddHttpClient(ApiNames.DeviceData, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.DeviceData]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.DeviceDefinitions, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.DeviceDefinitions]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.Devices, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Devices]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.Events, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Events]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.Trips, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Trips]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.User, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.User]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.Valuations, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.Valuations]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            services.AddHttpClient(ApiNames.VehicleSignalDecoding, client =>
            {
                client.BaseAddress = new Uri(Constants.ApiUrls[environment][ApiNames.VehicleSignalDecoding]);
                client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
            });
            
            return services;
        }
        
        public static IServiceCollection AddCoreServices(this IServiceCollection services, DimoEnvironment environment, Func<ClientCredentials, ClientCredentials> credentials)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
        
        public static IServiceCollection AddCoreServices(this IServiceCollection services, DimoEnvironment environment, ClientCredentials credentials)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
        
        
    }
}