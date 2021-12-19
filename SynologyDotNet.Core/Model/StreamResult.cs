using System.IO;
using System.Threading;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// ReadStreamArgs
    /// </summary>
    public class StreamResult
    {
        /// <summary>
        /// The stream to read. 
        /// </summary>
        /// <value>
        /// The stream.
        /// </value>
        public Stream Stream { get; }

        /// <summary>
        /// Gets the type of the content in the stream.
        /// </summary>
        /// <value>
        /// The type of the content in the stream.
        /// </value>
        public string ContentType { get; }

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        /// <value>
        /// The length of the stream.
        /// </value>
        public long ContentLength { get; }

        /// <summary>
        /// Gets the cancellation token.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamResult"/> class.
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <param name="contentType">Type of the content</param>
        /// <param name="contentLength">Length of the content</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public StreamResult(Stream stream, string contentType, long contentLength, CancellationToken cancellationToken)
        {
            Stream = stream;
            ContentType = contentType;
            ContentLength = contentLength;
            CancellationToken = cancellationToken;
        }
    }
}
