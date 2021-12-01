using System;
using System.Linq;
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
        /// <param name="baseAddress">The base address.</param>
        /// <param name="sslProtocols">The SSL protocols.</param>
        /// <param name="bypassSslCertificateValidation"></param>
        /// <returns></returns>
        public static HttpClient CreateHttpClient(Uri baseAddress, SslProtocols sslProtocols, bool bypassSslCertificateValidation)
        {
            HttpClient client;
            if (bypassSslCertificateValidation)
            {
                var handler = new HttpClientHandler
                {
                    UseCookies = false,
                    SslProtocols = sslProtocols,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true //Bypass certificate validation for HTTPS connections
                };
                client = new HttpClient(handler);
            }
            else
            {
                client = new HttpClient();
            }
            client.BaseAddress = baseAddress;
            client.Timeout = Timeout.InfiniteTimeSpan;

            // Set Accept headers
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");

            // Set User agent
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; EN; rv:11.0) like Gecko");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1"); //Todo: Is this really necessary?
            return client;
        }
    }
}
