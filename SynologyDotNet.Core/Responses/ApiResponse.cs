using Newtonsoft.Json;
using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.Responses
{
    public class ApiResponse : IApiResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public SynoError Error { get; set; }

        [JsonIgnore]
        public string ErrorDescription => (Error?.Code > 0 == true) ? GetErrorDescription(Error.Code) : string.Empty;

        public override string ToString() => Success ? "OK" : $"Error {Error.Code}: {ErrorDescription}";

        protected virtual string GetErrorDescription(int errorCode) => EnumHelper.GetEnumDescription<CommonErrorCode>(errorCode);
    }
}
