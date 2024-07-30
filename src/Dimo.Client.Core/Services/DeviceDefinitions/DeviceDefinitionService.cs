using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.DeviceDefinitions
{
    internal sealed class DeviceDefinitionService : IDeviceDefinitionsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DeviceDefinitionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<DeviceDefinition> GetByMmyAsync(string make, string model, int year, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeviceDefinition> GetByIdAsync(string deviceDefinitionId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<DeviceMake>> GetDeviceMakesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task GetDeviceTypeByIdAsync(string deviceTypeId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
