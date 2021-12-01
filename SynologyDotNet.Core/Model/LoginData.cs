using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    public struct LoginData
    {
        [JsonProperty("sid")]
        public string SID { get; set; }
        [JsonProperty("synotoken")]
        public string SynoToken { get; set; }
        [JsonProperty("is_portal_port")]
        public bool IsPortalPort { get; set; }
    }
}
