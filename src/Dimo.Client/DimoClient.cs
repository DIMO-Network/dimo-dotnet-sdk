using System;
using System.Threading.Tasks;
using Dimo.Client.Core;
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
using Dimo.Client.Streamr;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client
{
    public interface IDimoClient : IAsyncDisposable, IDisposable
    {
        IAuthenticationService AuthenticationService { get; }
        IDeviceDataService DeviceDataService { get; }
        IDeviceDefinitionsService DeviceDefinitionsService { get; }
        IDevicesService DevicesService { get; }
        IEventsService EventsService { get; }
        ITokenExchangeService TokenExchangeService { get; }
        ITripsService TripsService { get; }
        IUsersService UsersService { get; }
        IValuationsService ValuationsService { get; }
        IVehicleSignalDecodingService VehicleSignalDecodingService { get; }
        
    }

    internal class DimoClient : IDimoClient
    {
        public IAuthenticationService AuthenticationService => _provider.GetRequiredService<IAuthenticationService>();
        public IDeviceDataService DeviceDataService => _provider.GetRequiredService<IDeviceDataService>();
        public IDeviceDefinitionsService DeviceDefinitionsService => _provider.GetRequiredService<IDeviceDefinitionsService>();
        public IDevicesService DevicesService => _provider.GetRequiredService<IDevicesService>();
        public IEventsService EventsService => _provider.GetRequiredService<IEventsService>();
        public ITokenExchangeService TokenExchangeService => _provider.GetRequiredService<ITokenExchangeService>();
        public ITripsService TripsService => _provider.GetRequiredService<ITripsService>();
        public IUsersService UsersService => _provider.GetRequiredService<IUsersService>();
        public IValuationsService ValuationsService => _provider.GetRequiredService<IValuationsService>();
        public IVehicleSignalDecodingService VehicleSignalDecodingService => _provider.GetRequiredService<IVehicleSignalDecodingService>();

        private readonly ServiceProvider _provider;

        public DimoClient(DimoEnvironment environment, bool coreServices, bool identityApi, bool telemetryApi, bool streamr)
        {
            var prov = new ServiceCollection();
            
            if (coreServices)
            {
                prov.AddCoreServices(environment);
            }
            /*
            if (identityApi)
            {
                prov.AddIdentityApi();
            }
            
            if (telemetryApi)
            {
                prov.AddTelemetryApi();
            }
            */
            if (streamr)
            {
                prov.AddStreamr();
            }
            
            _provider = prov.BuildServiceProvider();
        }
        public void Dispose()
        {
            // TODO release managed resources here
            _provider.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            // TODO release managed resources here
            await _provider.DisposeAsync();
        }
    }
}
