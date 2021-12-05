using Newtonsoft.Json;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// Base class for list responses supporting pagination.
    /// </summary>
    public abstract class ListResponseBase
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
