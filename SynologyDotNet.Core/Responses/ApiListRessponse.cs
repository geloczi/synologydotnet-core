using Newtonsoft.Json;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// API response with a list data object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SynologyDotNet.Core.Responses.ApiResponse" />
    /// <seealso cref="SynologyDotNet.Core.Responses.IApiListResponse" />
    public class ApiListRessponse<T> : ApiResponse, IApiListResponse
        where T : ListResponseBase
    {
        ListResponseBase IApiListResponse.Data => Data;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
