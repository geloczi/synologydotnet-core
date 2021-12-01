using Newtonsoft.Json;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    public class ApiDataResponse<T> : ApiResponse
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        public ApiDataResponse()
        {
        }

        public ApiDataResponse(bool success, SynoError error, T data)
            : this()
        {
            Success = success;
            Error = error;
            Data = data;
        }

        public ApiDataResponse(IApiResponse source, T data)
            : this(source.Success, source.Error, data)
        {
        }
    }
}
