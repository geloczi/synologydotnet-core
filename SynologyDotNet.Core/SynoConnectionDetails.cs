using System;
using System.Threading.Tasks;
using SynologyDotNet.Core.Helpers;

namespace SynologyDotNet
{
    /// <summary>
    /// Provides the connection details to instantiate a <see cref="SynoClient"/>
    /// </summary>
    public class SynoConnectionDetails
    {
        internal Task<Uri> Endpoint { get; private set; }
        internal SynoClientOptions Options { get;  private set; }

        /// <summary>
        /// Creates connection details for the given URI.
        /// Make sure it also contains the correct port number (By default it is 5000/5001 for HTTP/HTTPS)
        /// </summary>
        /// <param name="server">The host including the port of the synology</param>
        /// <param name="disableCertificateValidation">Optional flag to disable the ssl certificate validation</param>
        public static SynoConnectionDetails ForUri(Uri server, bool disableCertificateValidation = false)
        {
            return new SynoConnectionDetails()
            {
                Endpoint = Task.FromResult<Uri>(server),
                Options = new SynoClientOptions(false, disableCertificateValidation),
            };
        }
        
        /// <summary>
        /// Creates connection details for the given QuickConnect id.
        /// </summary>
        /// <param name="quickConnectId">The QuickConnect id of the synology</param>
        public static SynoConnectionDetails ForQuickConnectId(string quickConnectId)
        {
            return new SynoConnectionDetails()
            {
                Endpoint = QuickConnectUriResolver.Resolve(quickConnectId),
                Options = new SynoClientOptions(true, false),
            };
        }
    }
}