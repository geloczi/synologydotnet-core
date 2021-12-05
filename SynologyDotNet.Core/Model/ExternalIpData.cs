using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// ExternalIpData
    /// </summary>
    public struct ExternalIpData
    {
        /// <summary>
        /// Gets or sets the DDNS hostname.
        /// </summary>
        /// <value>
        /// The DDNS hostname.
        /// </value>
        [JsonProperty("ddns_hostname")]
        public string DdnsHostname { get; set; }

        /// <summary>
        /// Gets or sets the external ip.
        /// </summary>
        /// <value>
        /// The external ip.
        /// </value>
        [JsonProperty("external_ip")]
        public string ExternalIp { get; set; }
    }
}
