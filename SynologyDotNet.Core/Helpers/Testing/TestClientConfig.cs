namespace SynologyDotNet.Core.Helpers.Testing
{
    /// <summary>
    /// Connection parameters for test clients.
    /// </summary>
    public class TestClientConfig
    {
        public string Server { get; set; } = "http://MySynology:5000";
        public string Username { get; set; } = "TestUser";
        public string Password { get; set; } = string.Empty;
    }
}
