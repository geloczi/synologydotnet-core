using Newtonsoft.Json;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// API response from the Synology API.
    /// </summary>
    public interface IApiResponse
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IApiResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("success")]
        bool Success { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [JsonProperty("error")]
        SynoError Error { get; }
    }
}
