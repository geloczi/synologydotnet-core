using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// LoginData
    /// </summary>
    public struct LoginData
    {
        /// <summary>
        /// Gets or sets the sid.
        /// </summary>
        /// <value>
        /// The sid.
        /// </value>
        [JsonProperty("sid")]
        public string SID { get; set; }

        /// <summary>
        /// Gets or sets the syno token.
        /// </summary>
        /// <value>
        /// The syno token.
        /// </value>
        [JsonProperty("synotoken")]
        public string SynoToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is portal port.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is portal port; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("is_portal_port")]
        public bool IsPortalPort { get; set; }
    }
}
