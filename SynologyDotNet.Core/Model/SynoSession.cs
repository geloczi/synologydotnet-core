namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Synology session. You can persist this data after the user logged in, and re-use it for the next login.
    /// </summary>
    public class SynoSession
    {
        /// <summary>
        /// Gets or sets the session name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the session token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the authentication cookie.
        /// </summary>
        /// <value>
        /// The cookie.
        /// </value>
        public string[] Cookie { get; set; }
    }
}
