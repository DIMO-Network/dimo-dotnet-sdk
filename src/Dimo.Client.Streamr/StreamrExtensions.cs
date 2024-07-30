using Microsoft.Extensions.DependencyInjection;

namespace Dimo.Client.Streamr
{
    public static class StreamrExtensions
    {
        public static IServiceCollection AddStreamr(this IServiceCollection services)
        {
            
            return services;
        }
        
        public static IServiceCollection AddStreamr(this IServiceCollection services, StreamrOptions options)
        {
            return services;
        }
    }
}