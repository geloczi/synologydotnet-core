using System;

namespace SynologyDotNet
{
    /// <summary>
    /// The base class to implement a Synology API connector.
    /// You max derive from this class and start implementing your API connector.
    /// </summary>
    public abstract class StationConnectorBase : IDisposable, ISynoClientConnectable
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
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

        #endregion Properties

        #region Public Methods

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

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// You must override this method and return the API names used by your component.
        /// </summary>
        /// <returns></returns>
        protected abstract string[] GetImplementedApiNames();

        /// <summary>
        /// Override this method to run custom dispose code.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        #endregion Protected Methods

        #region ISynoClientConnectable

        void ISynoClientConnectable.SetClient(SynoClient client)
        {
            Client = client;
        }

        string[] ISynoClientConnectable.GetApiNames()
        {
            return GetImplementedApiNames();
        }

        #endregion ISynoClientConnectable
    }
}
