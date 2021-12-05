using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Responses;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// LoginResult
    /// </summary>
    public class LoginResult : ApiDataResponse<LoginData>
    {
        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns></returns>
        protected override string GetErrorDescription(int errorCode) => EnumHelper.GetEnumDescription<LoginErrorCode>(errorCode);
    }
}
