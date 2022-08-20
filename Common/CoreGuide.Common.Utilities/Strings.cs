using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.Common.Utilities
{
    public static class Strings
    {
        public struct DateFormats
        {
            public const string hhmmsstt = "hh:mm:ss tt";
            public const string ddMMyyyy = "dd-MM-yyyy";
            public const string hhmmss = "hh:mm:ss";
            public const string ddMMyyyyhhmmssm = "ddMMyyyyhhmmssm";
            public const string ddMMyyyyhhmmtt = "ddMMyyyyhhmmtt";
            public const string yyyyMMddhhmmss = "yyyyMMddhhmmss";
            public const string dMMMMyyyy = "d MMMM yyyy";
            public const string yyyyMMddTHHmmss = "yyyy-MM-ddTHH:mm:ss";
            public const string MMddyyyySlash = "MM/dd/yyyy";
            public const string ddMMyyyySlash = "dd/MM/yyyy";
        }

        public struct Cultures
        {
            public const string En = "en";
            public const string Ar = "ar";
            public const string Arabic = "Arabic";
            public const string English = "English";
            public const string EnUs = "en-us";
            public const string ArEg = "ar-eg";
        }

        public struct Regex
        {
            public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            public const string OrangeDial = @"^01([0-2,5])\d{8}$";
            public const string OrangeCashPin = @"^\d{6}$";
            public const string Landline = @"^[0-9]{8,10}$";
        }

        public struct Password
        {
            public const string PositionArray = "0123456789";
            public const string AlphabetCaps = "QWERTYUPASDFGHJKZXCVBNM";
            public const string AlphabetLow = "qwertyupasdfghjkzxcvbnm";
            public const string Numerics = "23456789";
            public const string Special = "*$-+?_&=!%{}/";
        }

        public struct ErrorDescriptions
        {
            public const string PasswordLengthMustBeGreaterFourCharacters = "Number of characters should be greater than 4.";
            public const string LanguageHeaderMissingOrInvalid = "The \"lang\" header is missing or invalid";
            public const string LanguageHeaderMissingOrInvalidErrorLog = "The \"lang\" header is missing or invalid in {{url}} URL";
            public const string TokenInvalid = "The access token is invalid";
            public const string TokenInvalidErrorLog = "The access token is invalid in {{url}} URL";
            public const string TokenExpired = "The access token is expired";
            public const string TokenExpiredErrorLog = "The access token is expired in {{url}} URL";
            public const string UserIsNotSetErrorLog = "The User is not set in {{url}} URL";
            public const string GeneralError = "Sorry! Error has happened";
            public const string DialIsNull = "Dial is null";
            public const string DialIsInvalid = "Dial is invalid";
            public const string PinIsNullOrEmpty = "Pin is null or empty";
            public const string PinNotValid = "Pin is not valid";
            public const string DataBodyIsMissing = "Data body is missing";
        }

        public struct Services
        {
            public const string PostVerb = "POST";
            public const string GetVerb = "GET";
            public const string XmlContentType = "application/xml";
            public const string JsonContentType = "application/json";
        }

        public struct SMSFormats
        {
            public const string BodyFormat = "{0:X04}";
        }

        public struct Numbers
        {
            public const string DoubleZero = "00";
            public const string TwoZero = "20";
            public const string Two = "2";
            public const string Zero = "0";
            public const int TwoZeroNumber = 20;
            public const int ZeroNumber = 0;
            public const int TwoNumber = 2;
            public const string One = "1";
            public const string MinusOne = "-1";
            public const int OneNumber = 1;
            public const string Twenty = "20";
            public const int TwentyNumber = 20;
        }

        public struct AppSettingKeys
        {
            public const string SMSTitle = "SMSTitle";
            public const string SMSUsername = "SMSUsername";
            public const string SMSPassword = "SMSPassword";
            public const string IsItTestEnviroment = "IsItTestEnviroment";
            public const string IsMongoEnabled = "IsMongoEnabled";
            public const string SMTPAddress = "SMTPAddress";
            public const string Port = "Port";
            public const string EmailFromAddress = "EmailFromAddress";
            public const string EmailFromUserName = "EmailFromUserName";
            public const string EmailFromPassword = "EmailFromPassword";
            public const string SecureIntegrationBusUrl = "SecureIntegrationBusUrl";
            public const string TestingMsisdn = "TestingMsisdn";
            public const string TestingUserId = "TestingUserId";
            public const string TestingUserEmail = "TestingUserEmail";
            public const string TestingNotificationPreference = "TestingNotificationPreference";
            public const string RetryTransactionsURL = "RetryTransactionsURL";
            public const string IsEnrichmentTestingEnvironment = "IsEnrichmentTestingEnvironment";
            public const string EnrichmentTestingDial = "EnrichmentTestingDial";
            public const string EricssonEnrichmentHeaderDecryptKey = "EricssonEnrichmentHeaderDecryptKey";
            public const string EnrichmentHeaderDecryptKey = "EnrichmentHeaderDecryptKey";
            public const string DeepLinkBaseURL = "DeepLinkBaseURL";
        }

        public struct Keys
        {
            public const string Language = "lang";
            public const string Token = "Token";
            public const string Dial = "Dial";
            public const string Password = "Password";
            public const string RequestErrorCodeHandlingCachedKey = "Request_Error____Code_Handl____ings";
            public const string Null = "null";
            public const string Zero = "0";

            public const string IntegrationsRequestErrorCodeHandlingCachedKey = "Int__e___G___ration_____s__Request_Error____Code_Handl____ings";
            public const string MaximumImageSizeInBytes = "MaximumImageSizeInBytes";
            public const string Msisdn = "MSISDN";
            public const string XMsisdn = "X-MSISDN";
        }


        public struct CommandXmlTags
        {
            public const string AutomaticGeneratedCommandTag = "<COMMAND xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
            public const string CommandTag = "<COMMAND>";
            public const string Utf16 = "utf-16";
            public const string Utf8 = "utf-8";
            public const string AutomaticGeneratedEncodingTag = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            public const string EncodingTag = "<?xml version=\"1.0\"?><!DOCTYPE COMMAND PUBLIC \" -//Ocam//DTD XML Command 1.0//EN\" \"xml/command.dtd\">";
            public const string XmlVersionTag = "<?xml version=\"1.0\"?>";
            public const string XmlVersionWithEncodingTag = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
        }

        public struct TokenConfigurationsKey
        {
            public const string AccessTokenSecretKey = "AccessTokenSecretKey";
            public const string TokenIssuer = "TokenIssuer";
            public const string TokenAudience = "TokenAudience";
            public const string AccessTokenExpirationTimeInMinutes = "AccessTokenExpirationTimeInMinutes";
        }


        public struct Characters
        {
            public const char QuestionMark = '?';
            public const string EmptySpace = "";
            public const string WhiteSpace = " ";
            public const string Comma = ",";
        }

        public struct Headers
        {
            public const string Authorization = "Authorization";
        }
        public struct MethodNames
        {
            public const string PayToMerchant = "PayToMerchant";
        }

        public struct JWTClaimNames
        {
            public const string Id = ClaimTypes.NameIdentifier;
            public const string UserName = ClaimTypes.Name;
            public const string Email = ClaimTypes.Email;
            public const string Role = ClaimTypes.Role;
        }
        public struct Roles
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
        public struct Policies
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }
    }
}

