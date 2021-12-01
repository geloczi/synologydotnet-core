using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    public struct ExternalIpData
    {
        [JsonProperty("ddns_hostname")]
        public string DdnsHostname { get; set; }
        [JsonProperty("external_ip")]
        public string ExternalIp { get; set; }
    }
}
