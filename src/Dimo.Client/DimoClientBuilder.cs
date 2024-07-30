using Dimo.Client.Core;

namespace Dimo.Client
{
    /// <summary>
    /// Used to build a DimoClient instance with the desired configuration.
    /// </summary>
    public class DimoClientBuilder
    {
        internal DimoEnvironment Environment { get; private set; }
        public DimoClientBuilder()
        {
            
        }
        
        /// <summary>
        /// Defines the target environment for the client.
        /// Can be Development, Staging, or Production.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder WithEnvironment(DimoEnvironment environment)
        {
            Environment = environment;
            return this;
        }
        
        /// <summary>
        /// Adds support for the GraphQL Identity API to the client.
        /// </summary>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddIdentityApi()
        {
            return this;
        }
        
        /// <summary>
        /// Adds support for the GraphQL Telemetry API to the client.
        /// </summary>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddTelemetryApi()
        {
            return this;
        }
        
        /// <summary>
        /// Add support for the Streamr API to the client.
        /// </summary>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddStreamr()
        {
            return this;
        }
        
        /// <summary>
        /// This method adds support for all DIMO APIs  to the client.
        /// </summary>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddAllServices()
        {
            return AddIdentityApi()
                .AddTelemetryApi()
                .AddStreamr();
        }
        
        
        /// <summary>
        /// Builds the DimoClient instance with the specified configuration.
        /// </summary>
        /// <returns><see cref="DimoClient"/></returns>
        public DimoClient Build()
        {
            return new DimoClient(Environment);
        }
    }
}