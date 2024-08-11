using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.VehicleSignalDecoding
{
    public interface IVehicleSignalDecodingService
    {
        Task<DeviceConfigUrls> ListConfigUrlsByVinAsync(string vin, CancellationToken cancellationToken = default);
        Task<PidConfig> GetPidConfigsAsync(string templateName, CancellationToken cancellationToken = default);
        Task<DeviceSetting> GetDeviceSettingsAsync(string templateName, CancellationToken cancellationToken = default);
        Task<string> GetDbcTextAsync(string templateName, CancellationToken cancellationToken = default);
        Task<DeviceStatus> GetDeviceStatusByAddressAsync(string address, CancellationToken cancellationToken = default);
        Task SetDeviceStatusByAddressAsync(string address, DeviceStatusConfig newDeviceStatus, CancellationToken cancellationToken = default);
        Task GetJobsByAddressAsync(string address, CancellationToken cancellationToken = default);
        Task GetPendingJobsByAddressAsync(string address, CancellationToken cancellationToken = default);
        Task SetJobStatusByAddressAsync(string address, string jobId, CancellationToken cancellationToken = default);
    }
}
