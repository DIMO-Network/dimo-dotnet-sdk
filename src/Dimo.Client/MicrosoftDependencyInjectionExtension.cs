using System;
using Dimo.Client.Core;
using Microsoft.Extensions.DependencyInjection;
using Dimo.Client.Models;

namespace Dimo.Client
{
    public static class MicrosoftDependencyInjectionExtension
    {
        public static IServiceCollection AddDimoClient(this IServiceCollection services, TargetEnvironment environment)
        {
            services.AddCoreServices();
            return services;
        }
    }
}
