using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SynologyDotNet.Core.Helpers
{
    /// <summary>
    /// Helps to resolve the URI to access a Synology using its quick connect id
    /// </summary>
    public static class QuickConnectUriResolver
    {
        /// <summary>
        /// Resolved the server for the given quick connect id by requesting connection details at quickconnect.to
        /// </summary>
        public static async Task<Uri> Resolve(string quickConnectId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://global.quickconnect.to/Serv.php");
            var content = new StringContent($@"[
    {{
        ""version"": 1,
        ""command"": ""request_tunnel"",
        ""stop_when_error"": false,
        ""stop_when_success"": true,
        ""id"": ""dsm_portal_https"",
        ""serverID"": ""{quickConnectId}"",
        ""is_gofile"": false
    }}
]", null, "application/x-www-form-urlencoded");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject(responseContent) as JArray;
            var relayRegion = data.First.SelectToken("env.relay_region").Value<string>();
            var controlHost = data.First.SelectToken("env.control_host").Value<string>();
            var strippedControlHost = controlHost.Substring(controlHost.IndexOf(".", StringComparison.Ordinal) + 1);

            return new Uri($"https://{quickConnectId}.{relayRegion}.{strippedControlHost}/");
        }
    }
}