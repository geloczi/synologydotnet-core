using Newtonsoft.Json;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    public class ApiListRessponse<T> : ApiResponse, IApiListResponse
        where T : ListResponseBase
    {
        ListResponseBase IApiListResponse.Data => Data;

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
