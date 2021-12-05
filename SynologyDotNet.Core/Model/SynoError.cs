using Newtonsoft.Json;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Synology Error
    /// </summary>
    public class SynoError
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => Code.ToString();
    }
}
