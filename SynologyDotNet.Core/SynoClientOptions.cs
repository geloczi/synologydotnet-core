namespace SynologyDotNet
{
    /// <summary>
    /// Options that are considered when creating the http client
    /// </summary>
    public class SynoClientOptions
    {
        internal SynoClientOptions(bool quickConnectTunnelMode, bool disableCertificateValidation)
        {
            QuickConnectTunnelMode = quickConnectTunnelMode;
            DisableCertificateValidation = disableCertificateValidation;
        }

        /// <summary>
        /// If set to <code>true</code> the http client is configured for quick connect usage
        /// </summary>
        public bool QuickConnectTunnelMode { get; }
        
        /// <summary>
        /// If set to <code>true</code> the certificate validation of the http client is disabled
        /// </summary>
        public bool DisableCertificateValidation { get; }
    }
}