using System;
using System.Threading.Tasks;
using Dimo.Client.Extensions;
using Dimo.Client.Services.Attestation;
using Dimo.Client.Services.Authentication;
using Dimo.Client.Services.DeviceDefinitions;
using Dimo.Client.Services.Devices;
using Dimo.Client.Services.Identity;
using Dimo.Client.Services.Telemetry;
using Dimo.Client.Services.TokenExchange;
using Dimo.Client.Services.Trips;
using Dimo.Client.Services.Valuations;
using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client
{
    public interface IDimoClient : IAsyncDisposable, IDisposable
    {
        IAttestationService AttestationService { get; }
        IAuthenticationService AuthenticationService { get; }
        IDeviceDefinitionsService DeviceDefinitionsService { get; }
        IDevicesService DevicesService { get; }
        ITokenExchangeService TokenExchangeService { get; }
        ITripsService TripsService { get; }
        IValuationsService ValuationsService { get; }
        IIdentityService IdentityService { get; }
        ITelemetryService TelemetryService { get; }
    }

    internal class DimoClient : IDimoClient
    {
        public IAttestationService AttestationService => _provider.GetRequiredService<IAttestationService>();
        public IAuthenticationService AuthenticationService => _provider.GetRequiredService<IAuthenticationService>();
        public IDeviceDefinitionsService DeviceDefinitionsService => _provider.GetRequiredService<IDeviceDefinitionsService>();
        public IDevicesService DevicesService => _provider.GetRequiredService<IDevicesService>();
        public ITokenExchangeService TokenExchangeService => _provider.GetRequiredService<ITokenExchangeService>();
        public ITripsService TripsService => _provider.GetRequiredService<ITripsService>();
        public IValuationsService ValuationsService => _provider.GetRequiredService<IValuationsService>();
        public IIdentityService IdentityService => _provider.GetRequiredService<IIdentityService>();
        public ITelemetryService TelemetryService => _provider.GetRequiredService<ITelemetryService>();
        
        private readonly ServiceProvider _provider;

        public DimoClient(
            DimoEnvironment environment,
            ClientCredentials credentials,
            bool restServices, 
            bool graphql)
        {
            var collection = new ServiceCollection();
            
            foreach (var apis in Constants.ApiUrls[environment])
            {
                collection.AddHttpClient(apis.Key, client =>
                {
                    client.BaseAddress = new Uri(apis.Value);
                    client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
                });
            }
            
            collection.AddAuthServices(environment);
            
            if (restServices)
            {
                collection.AddDimoRestServices();
            }
            
            if (graphql)
            {
                collection.AddDimoGraphQlServices();
            }
            
            if (credentials != null)
            {
                collection.Configure<ClientCredentials>(options =>
                {
                    options.ClientId = credentials.ClientId;
                    options.PrivateKey = credentials.PrivateKey;
                    options.Domain = credentials.Domain;
                });
            }
            
            _provider = collection.BuildServiceProvider();
        }
        
        public void Dispose()
        {
            if (_provider == null) return;
            _provider.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_provider == null) return;
            await _provider.DisposeAsync();
        }
        
        ~DimoClient()
        {
            Dispose();
        }
    }
}
