using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities
{
    public static class BusinessStrings
    {
        public struct Resources
        {
            public struct ErrorMessagesKeys
            {
                public const string FileNameNotSpecified = "File route and directory name are not specified";
                public const string FileNotFound = "File is not found";
                public const string DirectoryNameNotSpecified = "Directory name is not specified";
                public const string URLNameNotSpecified = "URL name is not specified";
                public const string FileDownloadFailed = "Can't get the file from url: {0}";
                public const string FileExtensionInvalid = "Extension is not specified";
                public const string SalaryInvalid = "SalaryInvalid";
                public const string DepartmentInvalid = "DepartmentInvalid";
                public const string ManagerInvalid = "ManagerInvalid";
                public const string FileSizeInvalid = "File size should not be over {0} MBs";
                public const string FileTypeInvalid = "The file type {0} is not allowed, the allowed types are {1}";
                public const string CurrentPasswordNull = "CurrentPasswordNull";
                public const string CurrentPasswordInvalid = "CurrentPasswordInvalid";
                public const string CurrentPasswordWrong = "CurrentPasswordWrong";
                public const string NewPasswordNull = "NewPasswordNull";
                public const string NewPasswordInvalid = "NewPasswordInvalid";
                public const string NewpasswordEqualsCurrent = "NewpasswordEqualsCurrent";
                public const string UserNotFound = "UserNotFound";
                public const string PasswordNull = "PasswordNull";
                public const string OTPNull = "OTPNull";
                public const string PasswordInvalid = "PasswordInvalid";
                public const string DialInvalid = "DialInvalid";
                public const string RegistrationSuccess = "RegistrationSuccess";
                public const string RegistrationFailed = "RegistrationFailed";
                public const string RegistrationSuccessOTP = "RegistrationSuccessOTP";
                public const string PasswordWrong = "PasswordWrong";
                public const string UserLockedOut = "UserLockedOut";
                public const string SignInSuccess = "SignInSuccess";
                public const string ChangePasswordSuccess = "ChangePasswordSuccess";
                public const string NewAccessTokenSuccess = "NewAccessTokenSuccess";
                public const string ForgetPasswordSuccess = "ForgetPasswordSuccess";
                public const string SMSNotSent = "SMSNotSent";
                public const string OTPInvalid = "OTPInvalid";
                public const string OTPExpired = "OTPExpired";
                public const string ConfirmOTPSuccess = "ConfirmOTPSuccess";
                public const string InputNull = "InputNull";
                public const string GeneralError = "GeneralError";
                public const string UserFound = "UserFound";
                public const string UserUpdated = "UserUpdated";
                public const string IdWrong = "IdWrong";
                public const string IdNull = "IdNull";
                public const string NameNull = "NameNull";
                public const string EmailInvalid = "EmailInvalid";
                public const string DialNotUnique = "DialNotUnique";
                public const string DialNull = "DialNull";
                public const string RegisterUserOTPSuccess = "RegisterUserOTPSuccess";
                public const string OTPResendSuccess = "OTPResendSuccess";
                public const string EmailNull = "EmailNull";
                public const string UserNameNull = "UserNameNull";
                public const string UserNameNotUnique = "UserNameNotUnique";
                public const string EmailNotSent = "EmailNotSent";
                public const string OTPNotFound = "OTPNotFound";
                public const string DialChanged = "DialChanged";
                public const string NameInvalid = "NameInvalid";
                public const string ConfirmedPasswordInvalid = "ConfirmedPasswordInvalid";
                public const string ConfirmedPasswordNull = "ConfirmedPasswordNull";
                public const string RefreshTokenFailed = "RefreshTokenFailed";
                public const string RefreshTokenExpired = "RefreshTokenExpired";
                public const string RefreshTokenInvalid = "RefreshTokenInvalid";
                public const string RefreshTokenNotFound = "Refresh token is not found";
                public const string TokenNotFound = "Token is not found";
                public const string DialUpdatedSuccessfully = "Dial is updated successfully";
            }
        }

        public struct DirectoryNames 
        {
            public const string ProfilePictures = "Profile_Pictures";
        }
    }
}
