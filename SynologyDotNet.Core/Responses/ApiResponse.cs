using Newtonsoft.Json;
using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// API response from the Synology API.
    /// </summary>
    public class ApiResponse : IApiResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request was successfull.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error returned from the API.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        [JsonProperty("error")]
        public SynoError Error { get; set; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [JsonIgnore]
        public string ErrorDescription => (Error?.Code > 0 == true) ? GetErrorDescription(Error.Code) : string.Empty;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Success ? "OK" : $"Error {Error.Code}: {ErrorDescription}";

        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns></returns>
        protected virtual string GetErrorDescription(int errorCode) => EnumHelper.GetEnumDescription<CommonErrorCode>(errorCode);
    }
}
