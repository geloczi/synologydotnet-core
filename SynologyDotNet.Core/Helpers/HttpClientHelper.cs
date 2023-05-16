using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;

namespace SynologyDotNet.Core.Helpers
{
    /// <summary>
    /// HttpClientHelper
    /// </summary>
    public static class HttpClientHelper
    {
        /// <summary>
        /// Gets the default request header values.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string[] GetDefaultRequestHeaderValues(HttpClient client, string key) => client.DefaultRequestHeaders.TryGetValues(key, out var values) ? values.ToArray() : null;
        /// <summary>
        /// Gets the default request header value.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetDefaultRequestHeaderValue(HttpClient client, string key) => client.DefaultRequestHeaders.TryGetValues(key, out var values) ? values.FirstOrDefault() : null;
        /// <summary>
        /// Sets the default request header value.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetDefaultRequestHeaderValue(HttpClient client, string key, string value)
        {
            client.DefaultRequestHeaders.Remove(key);
            if (!(value is null))
                client.DefaultRequestHeaders.Add(key, value);
        }
        /// <summary>
        /// Sets the default request header values.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="key">The key.</param>
        /// <param name="values">The values.</param>
        public static void SetDefaultRequestHeaderValues(HttpClient client, string key, string[] values)
        {
            client.DefaultRequestHeaders.Remove(key);
            if (values?.Length > 0)
                client.DefaultRequestHeaders.Add(key, values);
        }

        /// <summary>
        /// Creates the HTTP client.
        /// </summary>
        public static HttpClient CreateHttpClient(Uri baseAddress, SslProtocols sslProtocols, SynoClientOptions options)
        {
            var handler = new HttpClientHandler()
            {
                SslProtocols = sslProtocols,
            };

            if (options.DisableCertificateValidation)
            {
                handler.UseCookies = false;
                /* Bypass certificate validation for HTTPS connections */
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            }
            
            if (options.QuickConnectTunnelMode)
            {
                handler.UseCookies = true;
                /* This cookie must be set when using the quickconnect service as relay */
                handler.CookieContainer = new CookieContainer();
                handler.CookieContainer.Add(new Cookie("type", "tunnel", "/", baseAddress.Host));
            }

            var client = new HttpClient(handler);

            client.BaseAddress = baseAddress;
            client.Timeout = Timeout.InfiniteTimeSpan;

            // Set Accept headers
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");

            // Set User agent
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; EN; rv:11.0) like Gecko");
            return client;
        }
    }
}
