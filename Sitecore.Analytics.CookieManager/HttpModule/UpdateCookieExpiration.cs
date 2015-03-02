using System;
using System.Linq;
using System.Web;

namespace Sitecore.Analytics.CookieManager.HttpModule
{
    /// <summary>
    /// TODO:
    /// Add the following line in the <system.webServer><modules></modules></system.webServer> section in the web.config file
    /// <add type="Sitecore.Analytics.CookieManager.HttpModule.UpdateCookieExpiration, Sitecore.Analytics.CookieManager" name="UpdateCookieExpiration" />
    /// </summary>
    public class UpdateCookieExpiration : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            //hook end request
            context.EndRequest += new EventHandler(OnEndRequest);
        }

        void OnEndRequest(object sender, EventArgs e)
        {
            HttpCookie cookieConsentCookie = HttpContext.Current.Request.Cookies[References.CookieManagerCookie];

            if (cookieConsentCookie != null && cookieConsentCookie.Value.Contains(References.ANALYTICS_COOKIE_DECLINE_VALUE))
            {
                //Overwrite the Sitecore Analytics cookies with a new cookie that expires in the past which will result in them being removed from the client
                foreach (string key in HttpContext.Current.Request.Cookies.AllKeys.Where(key => key.StartsWith(References.ANALYTICS_COOKIE_PREFIX)))
                {
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(key) { Expires = DateTime.Now.AddDays(-1) });
                }
            }
            else if (cookieConsentCookie != null && cookieConsentCookie.Value.Contains(References.ANALYTICS_COOKIE_ACCEPT_VALUE))
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[References.ANALYTICS_GLOBAL_COOKIE_NAME];
                if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && httpCookie.Expires > DateTime.Now.AddMonths(References.GlobalCookieDuration))
                    httpCookie.Expires = DateTime.Now.AddMonths(References.GlobalCookieDuration);
            }
            else
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies[References.ANALYTICS_GLOBAL_COOKIE_NAME];
                if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && httpCookie.Expires > DateTime.Now.AddMonths(References.GlobalCookieDuration))
                    httpCookie.Expires = DateTime.Now.AddMonths(References.GlobalCookieDuration);
            }
        }
    }
}