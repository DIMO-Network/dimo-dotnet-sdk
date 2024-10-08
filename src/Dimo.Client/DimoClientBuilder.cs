using System;
using System.Threading.Tasks;

namespace Dimo.Client
{
    /// <summary>
    /// Used to build a DimoClient instance with the desired configuration.
    /// </summary>
    public class DimoClientBuilder
    {
        internal DimoEnvironment Environment { get; private set; }
        internal ClientCredentials Credentials { get; private set; }
        internal bool RestServices { get; private set; }
        internal bool GraphqlServices { get; private set; }
        internal bool Streamr { get; private set; }
        
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
        public DimoClientBuilder AddGraphQLServices()
        {
            GraphqlServices = true;
            return this;
        }
        
        
        /// <summary>
        /// This method adds support for all REST DIMO APIs.
        /// </summary>
        /// <returns><see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddRestServices()
        {
            RestServices = true;
            return this;
        }
        
        /// <summary>
        /// This method adds support for all DIMO APIs  to the client.
        /// </summary>
        /// <returns>Returns the instance of <see cref="DimoClientBuilder"/></returns>
        public DimoClientBuilder AddAllServices()
        {
            return
                AddRestServices()
                .AddGraphQLServices();
        }

        public DimoClientBuilder WithCredentials(ClientCredentials credentials)
        {
            Credentials = credentials;
            return this;
        }

        public DimoClientBuilder WithCredentials(Action<ClientCredentials> config)
        {
            Credentials = new ClientCredentials();
            config(Credentials);
            return this;
        }
        
        /// <summary>
        /// Builds the DimoClient instance with the specified configuration.
        /// </summary>
        /// <returns>Returns an instance of <see cref="IDimoClient"/></returns>
        public IDimoClient Build()
        {
            return new DimoClient(Environment, Credentials, RestServices, GraphqlServices, Streamr);
        }
    }
}