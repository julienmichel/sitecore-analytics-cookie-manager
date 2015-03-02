using Sitecore.Data.Items;
using Sitecore.Web;
using System;
using System.Web;

namespace Sitecore.Analytics.CookieManager.layouts.sublayouts
{
	public partial class CookieMessage : System.Web.UI.UserControl
	{
        Item homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);

		protected void Page_Load(object sender, EventArgs e)
		{
            if (homeItem.Fields[References.SITECORE_ENFORCEPOLICY_FIELD] != null)
            {
                if (homeItem.Fields[References.SITECORE_ENFORCEPOLICY_FIELD].Value == "1")
                    cmCookieCheck();
            }
            else
                pnlCookieMessage.Visible = false;
		}

        protected void cmCookieCheck()
        {
            HttpCookie cmCookieRead = Request.Cookies[References.CookieManagerCookie];

            setCookieMessage();

            if (cmCookieRead == null) //first time into the site or cookies have been cleared
            {
                pnlCookieMessage.Visible = true;
                try
                {
                    //Show the Cookie Message for 6 months (~182 days), "show|accept"
                    writeCookie(string.Format("{0}|{1}", References.ANALYTICS_COOKIE_SHOW_VALUE, References.ANALYTICS_COOKIE_ACCEPT_VALUE), References.CookieManagerCookieMaxDuration);
                }
                catch
                {
                    //problem writing cookie - do nothing
                }
            }
            else
            {
                if (cmCookieRead.Value.ToString().Contains(References.ANALYTICS_COOKIE_HIDE_VALUE))
                    pnlCookieMessage.Visible = false;
            }
        }

        protected void setCookieMessage()
        {
            if (homeItem != null)
            {
                frCookiePolicyText.Item = homeItem;
                frAcceptCookiesText.Item = homeItem;
                frDeclineCookiesText.Item = homeItem;
                bool cookiePolicyPositionTop = homeItem.Fields[References.SITECORE_POLICYPOSITIONTOP_FIELD].HasValue;

                if (cookiePolicyPositionTop)
                {
                    divCookieMessage.Style.Remove("bottom");
                    divCookieMessage.Style.Add("top", "0px");
                }

                if (homeItem.Fields[References.SITECORE_FINDOUTMORETEXT_FIELD].HasValue)
                    hlFindOutMore.Text = homeItem.Fields[References.SITECORE_FINDOUTMORETEXT_FIELD].Value;

                if (homeItem.Fields[References.SITECORE_FINDOUTMOREPAGE_FIELD].HasValue)
                    hlFindOutMore.NavigateUrl = redirectURL(homeItem, References.SITECORE_FINDOUTMOREPAGE_FIELD).ToLower();
            }
        }

        protected void lbAcceptCookiesText_Click(object sender, EventArgs e)
        {
            try
            {
                //Hide the Cookie Message for 6 months (~182 days), "hide|accept"
                writeCookie(string.Format("{0}|{1}", References.ANALYTICS_COOKIE_HIDE_VALUE, References.ANALYTICS_COOKIE_ACCEPT_VALUE), References.CookieManagerCookieMaxDuration);
                pnlCookieMessage.Visible = false;
            }
            catch
            {
                //problem writing cookie - do nothing
            }

            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void lbDeclineCookiesText_Click(object sender, EventArgs e)
        {
            try
            {
                //Hide the Cookie Message for one week, "hide|decline"
                Response.Cookies.Remove(References.CookieManagerCookie);
                writeCookie(string.Format("{0}|{1}", References.ANALYTICS_COOKIE_HIDE_VALUE, References.ANALYTICS_COOKIE_DECLINE_VALUE), References.CookieManagerCookieMinDuration);
                pnlCookieMessage.Visible = false;
            }
            catch
            {
                //problem writing cookie - do nothing
            }

            Response.Redirect(Request.Url.PathAndQuery);
        }

        protected void writeCookie(string value, int duration)
        {
            HttpCookie cmCookie = new HttpCookie(References.CookieManagerCookie);
            cmCookie.Value = value;
            cmCookie.Expires = DateTime.Now.AddDays(duration);
            Response.Cookies.Add(cmCookie);
        }

        /// <summary>
        /// Returns the url of the given item's specified link field
        /// </summary>
        /// <param name="item"></param>
        /// <param name="sitecoreField"></param>
        /// <returns></returns>
        protected string redirectURL(Item item, string sitecoreField)
        {
            try
            {
                Sitecore.Data.Fields.LinkField linkField = item.Fields[sitecoreField];
                switch (linkField.LinkType.ToLower())
                {
                    case "internal":
                        // Use LinkMananger for internal links, if link is not empty
                        return linkField.TargetItem != null ? Sitecore.Links.LinkManager.GetItemUrl(linkField.TargetItem) : string.Empty;
                    case "media":
                        // Use MediaManager for media links, if link is not empty
                        return linkField.TargetItem != null ? Sitecore.Resources.Media.MediaManager.GetMediaUrl(linkField.TargetItem) : string.Empty;
                    case "external":
                        // Just return external links
                        return linkField.Url != null ? linkField.Url : string.Empty;
                    case "anchor":
                        // Prefix anchor link with # if link if not empty
                        return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                    case "mailto":
                        // Just return mailto link
                        return linkField.Url != null ? linkField.Url : string.Empty;
                    case "javascript":
                        return linkField.TargetItem == null ? string.Empty : WebUtil.GetFullUrl(Sitecore.Links.LinkManager.GetItemUrl(linkField.TargetItem));
                    default:
                        // URL not configured (may just be left blank)
                        return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
	}
}