namespace SynologyDotNet.Core.Helpers.Testing
{
    /// <summary>
    /// Connection parameters for test clients.
    /// </summary>
    public class TestClientConfig
    {
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>
        /// The server.
        /// </value>
        public string Server { get; set; } = "http://MySynology:5000";
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; } = "TestUser";
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; } = string.Empty;
    }
}
