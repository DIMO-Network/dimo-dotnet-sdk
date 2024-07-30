using System.Net.Http;

namespace Dimo.Client.Core.Services.VehicleSignalDecoding
{
    internal sealed class VehicleSignalDecodingService : IVehicleSignalDecodingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public VehicleSignalDecodingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}
