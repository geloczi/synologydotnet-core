using System;
using System.Net;
using System.Net.Http;

namespace SynologyDotNet.Core.Exceptions
{
    /// <summary>
    /// SynoHttpException
    /// </summary>
    public class SynoHttpException : Exception
    {
        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode StatusCode { get; }


        /// <summary>
        /// Gets the HTTP status reason phrase.
        /// </summary>
        /// <value>
        /// The reason phrase.
        /// </value>
        public string ReasonPhrase { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoHttpException"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public SynoHttpException(HttpResponseMessage msg) : base($"HTTP {(int)msg.StatusCode}, {msg.ReasonPhrase}.")
        {
            StatusCode = msg.StatusCode;
            ReasonPhrase = msg.ReasonPhrase;
        }
    }
}
