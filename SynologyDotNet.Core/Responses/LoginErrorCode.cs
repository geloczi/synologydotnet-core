using System.ComponentModel;

namespace SynologyDotNet.Core.Model
{
    /// <summary>
    /// Known Synology Error Codes regarding login.
    /// </summary>
    public enum LoginErrorCode
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
        /// The account parameter is not specified.
        /// </summary>
        [Description("The account parameter is not specified.")]
        AccountParameterMissing = 101,

        /// <summary>
        /// Authenticaion failed.
        /// </summary>
        [Description("Authenticaion failed.")]
        AuthenticaionFailed = 105,

        /// <summary>
        /// Invalid password.
        /// </summary>
        [Description("Invalid password.")]
        InvalidPassword = 400,

        /// <summary>
        /// Guest or disabled account.
        /// </summary>
        [Description("Guest or disabled account.")]
        GuestOrDisabledAccount = 401,

        /// <summary>
        /// Permission denied.
        /// </summary>
        [Description("Permission denied.")]
        PermissionDenied = 402,

        /// <summary>
        /// One time password not specified.
        /// </summary>
        [Description("One time password not specified.")]
        OneTimePasswordNotSpecified = 403,

        /// <summary>
        /// One time password authenticate failed.
        /// </summary>
        [Description("One time password authenticate failed.")]
        OneTimePasswordAuthenticateFailed = 404,

        /// <summary>
        /// App portal incorrect.
        /// </summary>
        [Description("App portal incorrect.")]
        AppPortalIncorrect = 405,

        /// <summary>
        /// OTP code enforced.
        /// </summary>
        [Description("OTP code enforced.")]
        OtpCodeEnforced = 406,

        /// <summary>
        /// Exceeded max tries.
        /// </summary>
        [Description("Exceeded max tries.")]
        ExceededMaxTries = 407,

        /// <summary>
        /// Password expired can not change.
        /// </summary>
        [Description("Password expired can not change.")]
        PasswordExpiredCannotChange = 408,

        /// <summary>
        /// Password Expired.
        /// </summary>
        [Description("Password Expired.")]
        PasswordExpired = 409,

        /// <summary>
        /// Password must change.
        /// </summary>
        [Description("Password must change.")]
        PasswordMustChange = 410,

        /// <summary>
        /// Account locked.
        /// </summary>
        [Description("Account locked.")]
        AccountLocked = 411
    }
}
