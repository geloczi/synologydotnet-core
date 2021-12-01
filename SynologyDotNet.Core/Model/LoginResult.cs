using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Responses;

namespace SynologyDotNet.Core.Model
{
    public class LoginResult : ApiDataResponse<LoginData>
    {
        protected override string GetErrorDescription(int errorCode) => EnumHelper.GetEnumDescription<LoginErrorCode>(errorCode);
    }
}
