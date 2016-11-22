namespace GraphConsoleAppV3
{
    internal class AppModeConstants
    {
        public const string ClientId = "2ec9eead-19e6-455e-a921-791eda349332";
        public const string ClientSecret = "FfweNSqFhp8gzIH7PuYNbHXPt+M8Hk6sTCz+Xa1GtUU=";
        public const string TenantName = "karen.onMicrosoft.com";
        public const string TenantId = "e4162ad0-e9e3-4a16-bf40-0d8a906a06d4";
        public const string AuthString = GlobalConstants.AuthString + TenantName;
    }

    internal class UserModeConstants
    {
        public const string TenantId = AppModeConstants.TenantId;
        public const string ClientId = "2ec9eead-19e6-455e-a921-791eda349332";
        public const string AuthString = GlobalConstants.AuthString + "common/";
    }

    internal class GlobalConstants
    {
        public const string AuthString = "https://login.microsoftonline.com/";        
        public const string ResourceUrl = "https://graph.windows.net";
        public const string GraphServiceObjectId = "00000002-0000-0000-c000-000000000000";
    }
}
