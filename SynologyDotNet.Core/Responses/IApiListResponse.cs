using Newtonsoft.Json;

namespace SynologyDotNet.Core.Responses
{
    public interface IApiListResponse : IApiResponse
    {
        [JsonProperty("data")]
        ListResponseBase Data { get; }
    }
}
