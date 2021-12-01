using System;
using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Exceptions
{
    /// <summary>
    /// SynoLoginException
    /// </summary>
    /// <seealso cref="Exception" />
    public class SynoLoginException : Exception
    {
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynoLoginException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public SynoLoginException(int errorCode) : base(EnumHelper.GetEnumDescription<LoginErrorCode>(errorCode))
        {
            ErrorCode = errorCode;
        }
    }
}
