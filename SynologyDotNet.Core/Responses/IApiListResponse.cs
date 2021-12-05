using Newtonsoft.Json;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// API response from the Synology API with a list data object.
    /// </summary>
    public interface IApiListResponse : IApiResponse
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        ListResponseBase Data { get; }
    }
}
