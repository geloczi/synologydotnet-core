using System.ComponentModel;

namespace SynologyDotNet.Core.Responses
{
    /// <summary>
    /// Error codes returned by a standard Synology API.
    /// </summary>
    public enum CommonErrorCode
    {
        /// <summary>
        /// None
        /// </summary>
        [Description("")]
        None = 0,

        /// <summary>
        /// Unknown error.
        /// </summary>
        [Description("Unknown error.")]
        UnknownError = 100,

        /// <summary>
        /// No parameter of API, method or version.
        /// </summary>
        [Description("No parameter of API, method or version.")]
        NoParameterOfApiMethodOrVersion = 101,

        /// <summary>
        /// The requested API does not exist.
        /// </summary>
        [Description("The requested API does not exist.")]
        TheRequestedApiDoesNotExist = 102,

        /// <summary>
        /// The requested method does not exist.
        /// </summary>
        [Description("The requested method does not exist.")]
        TheRequestedMethodDoesNotExist = 103,

        /// <summary>
        /// The requested version does not support the functionality.
        /// </summary>
        [Description("The requested version does not support the functionality.")]
        TheRequestedVersionDoesNotSupportTheFunctionality = 104,

        /// <summary>
        /// The logged in session does not have permission.
        /// </summary>
        [Description("The logged in session does not have permission.")]
        TheLoggedInSessionDoesNotHavePermission = 105,

        /// <summary>
        /// Session timeout.
        /// </summary>
        [Description("Session timeout.")]
        SessionTimeout = 106,

        /// <summary>
        /// Session interrupted by duplicated login.
        /// </summary>
        [Description("Session interrupted by duplicated login.")]
        SessionInterruptedByDuplicatedLogin = 107,

        /// <summary>
        /// Failed to upload the file.
        /// </summary>
        [Description("Failed to upload the file.")]
        FailedToUploadTheFile = 108,

        /// <summary>
        ///  network connection is unstable or the system is busy.
        /// </summary>
        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy = 109,

        /// <summary>
        /// The network connection is unstable or the system is busy.
        /// </summary>
        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy2 = 110,

        /// <summary>
        /// The network connection is unstable or the system is busy.
        /// </summary>
        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy3 = 111,

        /// <summary>
        /// Preserve for other purpose.
        /// </summary>
        [Description("Preserve for other purpose.")]
        PreservedForOtherPurpose1 = 112,

        /// <summary>
        /// Preserve for other purpose.
        /// </summary>
        [Description("Preserve for other purpose.")]
        PreservedForOtherPurpose2 = 113,

        /// <summary>
        /// Lost parameters for this API.
        /// </summary>
        [Description("Lost parameters for this API.")]
        LostParametersForThisApi = 114,

        /// <summary>
        /// Not allowed to upload a file.
        /// </summary>
        [Description("Not allowed to upload a file.")]
        NotAllowedToUploadFile = 115,

        /// <summary>
        /// Not allowed to perform for a demo site.
        /// </summary>
        [Description("Not allowed to perform for a demo site.")]
        NotAllowedToPerformForDemoSite = 116,

        /// <summary>
        /// The network connection is unstable or the system is busy.
        /// </summary>
        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy4 = 117,

        /// <summary>
        /// The network connection is unstable or the system is busy.
        /// </summary>
        [Description("The network connection is unstable or the system is busy.")]
        TheNetworkConnectionIsUnstableOrTheSystemIsBusy5 = 118,

        /// <summary>
        /// Invalid session.
        /// </summary>
        [Description("Invalid session.")]
        InvalidSession = 119,

        /// <summary>
        /// Request source IP does not match the login IP.
        /// </summary>
        [Description("Request source IP does not match the login IP.")]
        RequestSourceIpDoesNotMatchTheLoginIp = 150,

        // Preserve for other purpose (based on official Developer's Guide)
        // 112
        // 113
        // 120-149
    }
}
