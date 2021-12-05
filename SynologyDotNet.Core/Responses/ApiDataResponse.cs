using Newtonsoft.Json;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// API response with a data object.
    /// </summary>
    /// <typeparam name="T">Type of the data object returned by the API.</typeparam>
    public class ApiDataResponse<T> : ApiResponse
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDataResponse{T}"/> class.
        /// </summary>
        public ApiDataResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDataResponse{T}"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="error">The error.</param>
        /// <param name="data">The data.</param>
        public ApiDataResponse(bool success, SynoError error, T data)
            : this()
        {
            Success = success;
            Error = error;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDataResponse{T}"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="data">The data.</param>
        public ApiDataResponse(IApiResponse source, T data)
            : this(source.Success, source.Error, data)
        {
        }
    }
}
