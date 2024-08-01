using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Core.Models;

namespace Dimo.Client.Core.Services.DeviceDefinitions
{
    public interface IDeviceDefinitionsService
    {
        Task<DeviceDefinition> GetByMmyAsync(string make, string model, int year, CancellationToken cancellationToken = default);
        Task<DeviceDefinition> GetByIdAsync(string deviceDefinitionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<DeviceMake>> GetDeviceMakesAsync(CancellationToken cancellationToken = default);
        Task<DeviceType> GetDeviceTypeByIdAsync(string deviceTypeId, CancellationToken cancellationToken = default);
    }
}
