using Sitecore.Configuration;

namespace Sitecore.Analytics.CookieManager
{
    public static class References
    {
        private const int ANALYTICS_GLOBAL_COOKIE_DURATION_DEFAULT = 12;

        public const string ANALYTICS_GLOBAL_COOKIE_NAME = "SC_ANALYTICS_GLOBAL_COOKIE";
        public const string ANALYTICS_COOKIE_PREFIX = "SC_ANALYTICS";
        public const string ANALYTICS_COOKIE_ACCEPT_VALUE = "accept";
        public const string ANALYTICS_COOKIE_DECLINE_VALUE = "decline";
        public const string ANALYTICS_COOKIE_SHOW_VALUE = "show";
        public const string ANALYTICS_COOKIE_HIDE_VALUE = "hide";
        
        public const string SITECORE_ENFORCEPOLICY_FIELD = "EnforceCookiePolicy";
        public const string SITECORE_POLICYPOSITIONTOP_FIELD = "CookiePolicyPositionTop";
        public const string SITECORE_FINDOUTMORETEXT_FIELD = "FindOutMoreText";
        public const string SITECORE_FINDOUTMOREPAGE_FIELD = "FindOutMorePage";

        public static int GlobalCookieDuration
        {
            get { return Settings.GetIntSetting("Analytics.GlobalCookieDuration", ANALYTICS_GLOBAL_COOKIE_DURATION_DEFAULT); }
        }

        public static int CookieManagerCookieMinDuration
        {
            get { return Settings.GetIntSetting("Analytics.CookieManagerCookieMinDuration", ANALYTICS_GLOBAL_COOKIE_DURATION_DEFAULT); }
        }

        public static int CookieManagerCookieMaxDuration
        {
            get { return Settings.GetIntSetting("Analytics.CookieManagerCookieMaxDuration", ANALYTICS_GLOBAL_COOKIE_DURATION_DEFAULT); }
        }

        public static string CookieManagerCookie
        {
            get { return Settings.GetSetting("Analytics.CookieManagerCookie"); }
        }
    }
}