using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client.Services.Events
{
    public interface IEventsService
    {
        Task<IReadOnlyCollection<DeviceEvent>> ListEventsAsync(string authToken, string deviceId = null, string deviceType = null, string subType = null, CancellationToken cancellationToken = default);
    }
}