using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    public struct ApiSpecification
    {
        [JsonProperty("maxVersion")]
        public int MaxVersion { get; set; }
        [JsonProperty("minVersion")]
        public int MinVersion { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("requestFormat")]
        public string RequestFormat { get; set; }
    }
}
