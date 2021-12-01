using System;
using System.Threading.Tasks;

namespace SynologyDotNet
{

    /// <summary>
    /// The base class to implement Synology API connectors.
    /// </summary>
    public abstract class StationConnectorBase : IDisposable, ISynoClientConnectable
    {
        protected bool IsDisposed { get; private set; }

        private SynoClient _client;
        /// <summary>
        /// The underlying client
        /// </summary>
        public SynoClient Client
        {
            get => _client;
            private set
            {
                if (_client is null)
                    _client = value;
                else
                    throw new InvalidOperationException("Client already connected.");
            }
        }

        /// <summary>
        /// Creates the connector re-using an existing client.
        /// </summary>
        public StationConnectorBase()
        {
        }

        /// <summary>
        /// Initializes this connector instance, downloads API metadata from the server.
        /// </summary>
        public async Task ConnectAsync(SynoClient client)
        {
            Client = client;
            await Client.LoadApiInfos(GetImplementedApiNames());
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
                return;
            OnDispose();
            IsDisposed = true;
        }

        /// <summary>
        /// You must override this method and return the API names used by your component.
        /// </summary>
        /// <returns></returns>
        protected abstract string[] GetImplementedApiNames();

        protected virtual void OnDispose()
        {
        }

        void ISynoClientConnectable.SetClient(SynoClient client)
        {
            Client = client;
        }

        string[] ISynoClientConnectable.GetApiNames()
        {
            return GetImplementedApiNames();
        }
    }
}
