using System;
using System.Net;
using System.Net.Http;

namespace SynologyDotNet.Core.Exceptions
{
    public class SynoHttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ReasonPhrase { get; }

        public SynoHttpException(HttpResponseMessage msg) : base($"HTTP {(int)msg.StatusCode}, {msg.ReasonPhrase}.")
        {
            StatusCode = msg.StatusCode;
            ReasonPhrase = msg.ReasonPhrase;
        }
    }
}
