using Newtonsoft.Json;

namespace SynologyDotNet.Core.Responses
{
    public abstract class ListResponseBase
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
