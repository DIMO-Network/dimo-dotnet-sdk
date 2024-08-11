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
using Dimo.Client.Graphql;
using Dimo.Client.Graphql.Services.Identity;
using Dimo.Client.Graphql.Services.Telemetry;
using Dimo.Client.Streamr;
using Microsoft.Extensions.DependencyInjection;
using GraphEnvironment = Dimo.Client.Graphql.DimoEnvironment;
using DimoEnvironment = Dimo.Client.Core.DimoEnvironment;
using StreamrEnvironment = Dimo.Client.Streamr.DimoEnvironment;

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
        IIdentityService IdentityService { get; }
        ITelemetryService TelemetryService { get; }
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
        public IIdentityService IdentityService => _provider.GetRequiredService<IIdentityService>();
        public ITelemetryService TelemetryService => _provider.GetRequiredService<ITelemetryService>();
        
        
        public IVehicleSignalDecodingService VehicleSignalDecodingService => _provider.GetRequiredService<IVehicleSignalDecodingService>();

        private readonly ServiceProvider _provider;

        public DimoClient(DimoEnvironment environment, bool coreServices, bool graphql, bool streamr)
        {
            var collection = new ServiceCollection();
            
            if (coreServices)
            {
                collection.AddCoreServices(environment);
            }
            
            if (graphql)
            {
                collection.AddGraphql((GraphEnvironment)environment);
            }
            
            if (streamr)
            {
                collection.AddStreamr((StreamrEnvironment)environment);
            }
            
            _provider = collection.BuildServiceProvider();
        }
        
        public void Dispose()
        {
            // TODO release managed resources here
            if (_provider == null) return;
            _provider.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            // TODO release managed resources here
            if (_provider == null) return;
            await _provider.DisposeAsync();
        }
        
        ~DimoClient()
        {
            Dispose();
        }
    }
}
