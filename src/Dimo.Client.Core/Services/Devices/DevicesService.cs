using System.Net.Http;

namespace Dimo.Client.Core.Services.Devices
{
    internal sealed class DevicesService : IDevicesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DevicesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
