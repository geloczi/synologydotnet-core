using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Synology Error
    /// </summary>
    public class SynoError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        public override string ToString() => Code.ToString();
    }
}
