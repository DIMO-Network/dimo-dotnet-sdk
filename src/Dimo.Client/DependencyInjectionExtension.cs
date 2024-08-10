using System;
using Dimo.Client.Core;
using Dimo.Client.Graphql;
using Microsoft.Extensions.DependencyInjection;
using Dimo.Client.Streamr;

using GraphEnvironment = Dimo.Client.Graphql.DimoEnvironment;
using DimoEnvironment = Dimo.Client.Core.DimoEnvironment;
using StreamrEnvironment = Dimo.Client.Streamr.DimoEnvironment;


namespace Dimo.Client
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDimoClient(this IServiceCollection services, DimoEnvironment environment)
        {
            services.AddCoreServices(environment);
            services.AddGraphql((GraphEnvironment)environment);
            services.AddStreamr((StreamrEnvironment)environment);
            return services;
        }
        
        
        
        public static IServiceCollection AddDimoClient(this IServiceCollection services, DimoEnvironment environment, Func<StreamrOptions> configureOptions)
        {
            services.AddCoreServices(environment);
            
            return services;
        }
    }
}
