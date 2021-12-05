using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Synology API specification
    /// </summary>
    public struct ApiSpecification
    {
        /// <summary>
        /// Gets or sets the maximum version.
        /// </summary>
        /// <value>
        /// The maximum version.
        /// </value>
        [JsonProperty("maxVersion")]
        public int MaxVersion { get; set; }

        /// <summary>
        /// Gets or sets the minimum version.
        /// </summary>
        /// <value>
        /// The minimum version.
        /// </value>
        [JsonProperty("minVersion")]
        public int MinVersion { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the request format.
        /// </summary>
        /// <value>
        /// The request format.
        /// </value>
        [JsonProperty("requestFormat")]
        public string RequestFormat { get; set; }
    }
}
