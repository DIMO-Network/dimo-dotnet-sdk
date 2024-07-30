using System.Net.Http;

namespace Dimo.Client.Core.Services.Events
{
    internal sealed class EventsService : IEventsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EventsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
    }
}