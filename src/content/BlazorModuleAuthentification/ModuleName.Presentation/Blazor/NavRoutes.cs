using System.Diagnostics.CodeAnalysis;

namespace ModuleName.Presentation.Blazor;

[ExcludeFromCodeCoverage]
public static class NavRoutes
{
    //TODO : identifier les routes qui ne doivent pas être publiques mais internes
    public const string AccountConfirmEmail = "/account/confirm-email";
    public const string AccountConfirmEmailChange = "/account/confirm-email-change";
    public const string AccountExternalLogin = "/account/external-login";
    public const string AccountForgotPassword = "/account/forgot-password";
    public const string AccountForgotPasswordConfirmation = "/account/forgot-password-confirmation";
    public const string AccountInvalidPasswordReset = "/account/invalid-password-reset";
    public const string AccountInvalidUser = "/account/invalid-user";
    public const string AccountLogin = "/account/login";
    public const string AccountLoginWith2fa = "/account/login-with-2fa";
    public const string AccountLoginWithRecoveryCode = "/account/login-with-recovery-code";
    public const string AccountLogout = "/account/logout";
    public const string AccountRegister = "/account/register";
    public const string AccountRegisterConfirmation = "/account/register-confirmation";
    public const string AccountResendEmailConfirmation = "/account/resend-email-confirmation";
    public const string AccountResetPassword = "/account/reset-password";
    public const string AccountResetPasswordConfirmation = "/account/reset-password-confirmation";

    public const string AccountManage = "/account/manage";
    public const string AccountManageChangePassword = "/account/manage/change-password";
    public const string AccountManageDeletePersonalData = "/account/manage/delete-personal-data";
    public const string AccountManageDisable2fa = "/account/manage/disable-2fa";
    public const string AccountManageEmail = "/account/manage/email";
    public const string AccountManageEnableAuthenticator = "/account/manage/enable-authenticator";
    public const string AccountManageExternalLogins = "/account/manage/external-logins";
    public const string AccountManageGenerateRecoveryCodes = "/account/manage/generate-recovery-codes";
    public const string AccountManagePersonalData = "/account/manage/personal-data";
    public const string AccountManageResetAuthenticator = "/account/manage/reset-authenticator";
    public const string AccountManageSetPassword = "/account/manage/set-password";
    public const string AccountManageTwoFactorAuthentication = "/account/manage/two-factor-authentication";

    public const string AdminAuthentication = "/admin/authentication";
}