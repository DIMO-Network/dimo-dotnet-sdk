using System;
using System.Threading.Tasks;
using Dimo.Client.Core;

namespace Dimo.Client
{
    public class DimoClient : IDisposable, IAsyncDisposable
    {
        private DimoEnvironment _environment;
        private bool _disposed;

        public DimoClient()
        {
            _environment = DimoEnvironment.Development;
        }

        public DimoClient(DimoEnvironment environment)
        {
            _environment = environment;
        }



        public void Dispose()
        {

        }

        public async ValueTask DisposeAsync()
        {

        }
    }
}
