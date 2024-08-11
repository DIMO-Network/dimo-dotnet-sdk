using System;
using Dimo.Client.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace Dimo.Client
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDimoClient(this IServiceCollection services,
            Action<DimoClientOptions> options)
        {
            var clientOptions = new DimoClientOptions();
            options(clientOptions);

            if (clientOptions.Credentials != null)
            {
                services.AddSingleton(clientOptions.Credentials);
            }
            
            foreach (var apis in Constants.ApiUrls[clientOptions.Environment])
            {
                services.AddHttpClient(apis.Key, client =>
                {
                    client.BaseAddress = new Uri(apis.Value);
                    client.DefaultRequestHeaders.Add("User-Agent", Constants.UserAgent);
                });
            }
            
            services.AddCoreServices(clientOptions.Environment);
            services.AddGraphql(clientOptions.Environment);
            
            return services;
        }
    }

    public class DimoClientOptions
    {
        public DimoEnvironment Environment { get; set; }
        public ClientCredentials Credentials { get; set; }
    }
}
