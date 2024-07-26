using System;
using System.Threading.Tasks;
using Dimo.Client.Models;

namespace Dimo.Client
{
    public class DimoClient : IDisposable, IAsyncDisposable
    {
        private TargetEnvironment _environment;
        private bool _disposed;

        public DimoClient()
        {
            _environment = TargetEnvironment.Development;
        }

        public DimoClient(TargetEnvironment environment)
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
