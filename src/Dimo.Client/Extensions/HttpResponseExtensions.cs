using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Dimo.Client.Extensions
{
    internal static class HttpResponseExtensions
    {
        internal static async Task ThrowIfFailedAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var error = await response.Content.ReadAsStringAsync();
            var message = JObject.Parse(error).SelectToken("message")?.ToString() ?? error;
            throw new HttpRequestException(message);
        }
    }
}