using System.ComponentModel;

namespace SynologyDotNet.Core.Responses
{
    public enum CommonErrorCode
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("")]
        None = 0,

        [Description("Unknown error.")]
        UnknownError = 100,

        [Description("No parameter of API, method or version.")]
        NoParameterOfApiMethodOrVersion = 101,

        [Description("The requested API does not exist.")]
        TheRequestedApiDoesNotExist = 102,

        [Description("The requested method does not exist.")]
        TheRequestedMethodDoesNotExist = 103,

        [Description("The requested version does not support the functionality.")]
        TheRequestedVersionDoesNotSupportTheFunctionality = 104,

        [Description("The logged in session does not have permission.")]
        TheLoggedInSessionDoesNotHavePermission = 105,

        [Description("Session timeout.")]
        SessionTimeout = 106,

        [Description("Session interrupted by duplicated login.")]
        SessionInterruptedByDuplicatedLogin = 107,

        [Description("Failed to upload the file.")]
        FailedToUploadTheFile = 108,

        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy = 109,

        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy2 = 110,

        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy3 = 111,

        [Description("Preserve for other purpose.")]
        PreservedForOtherPurpose1 = 112,

        [Description("Preserve for other purpose.")]
        PreservedForOtherPurpose2 = 113,

        [Description("Lost parameters for this API.")]
        LostParametersForThisApi = 114,

        [Description("Not allowed to upload a file.")]
        NotAllowedToUploadFile = 115,

        [Description("Not allowed to perform for a demo site.")]
        NotAllowedToPerformForDemoSite = 116,

        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy4 = 117,

        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy5 = 118,

        [Description("Invalid session.")]
        InvalidSession = 119,

        [Description("Request source IP does not match the login IP.")]
        RequestSourceIpDoesNotMatchTheLoginIp = 150,

        // Preserve for other purpose (based on official Developer's Guide)
        // 112
        // 113
        // 120-149
    }
}
