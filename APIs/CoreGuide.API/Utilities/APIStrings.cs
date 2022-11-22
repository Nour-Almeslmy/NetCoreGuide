namespace CoreGuide.API.Utilities
{
    public static class APIStrings
    {
        public struct ConfigurationsSections
        {
            public const string ConnectionString = "DefaultConnection";
            public const string IdentitySettings = "IdentitySettings";
            public const string AllowedFileSettings = "AllowedFileSettings";
            public const string AccessTokenSettings = "AccessTokenSettings";
            public const string RefreshTokenSettings = "RefreshTokenSettings";
            public const string AllowedOrigins = "AllowedOrigins";
            public const string Localization = "LocalizationSettings";
            public const string TempConvertService = "TempConvertServiceSettings";
        }
    }
}
