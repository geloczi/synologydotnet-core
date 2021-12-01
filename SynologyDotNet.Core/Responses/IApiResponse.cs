using Newtonsoft.Json;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    public interface IApiResponse
    {
        [JsonProperty("success")]
        bool Success { get; }

        [JsonProperty("error")]
        SynoError Error { get; }
    }
}
