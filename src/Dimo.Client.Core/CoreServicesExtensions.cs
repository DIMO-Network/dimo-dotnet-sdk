using Dimo.Client.Core.Services.DeviceData;
using Dimo.Client.Core.Services.DeviceDefinitions;
using Dimo.Client.Core.Services.TokenExchange;
using Dimo.Client.Core.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Core
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IDeviceDataService, DeviceDataService>();
            services.AddScoped<IDeviceDefinitionsService, DeviceDefinitionService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokenExchangeService, TokenExchangeService>();
            return services;
        }
    }
}