using System;
using Dimo.Client.Core;
using Microsoft.Extensions.DependencyInjection;
using Dimo.Client.Streamr;

namespace Dimo.Client
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDimoClient(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddCoreServices(environment);
            services.AddStreamr();
            return services;
        }
        
        
        
        public static IServiceCollection AddDimoClient(this IServiceCollection services, DimoEnvironment environment, Func<StreamrOptions> configureOptions)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
    }
}
