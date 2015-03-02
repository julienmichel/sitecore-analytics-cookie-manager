<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CookieMessage.ascx.cs" Inherits="Sitecore.Analytics.CookieManager.layouts.sublayouts.CookieMessage" %>
<%@ register tagprefix="sc" namespace="Sitecore.Web.UI.WebControls" assembly="Sitecore.Kernel" %>

<link href="/lib/css/cookie-manager.css" type="text/css" rel="stylesheet">

<asp:Panel runat="server" ID="pnlCookieMessage">
    <div id="divCookieMessage" runat="server" clientidmode="Static">
        <div id="cookieMessageContainer">
            <div id="cookieMessage">
                <sc:FieldRenderer ID="frCookiePolicyText" runat="server" FieldName="CookiePolicyText" />
                <asp:LinkButton ID="lbAcceptCookiesText" runat="server" OnClick="lbAcceptCookiesText_Click">
                    <sc:FieldRenderer ID="frAcceptCookiesText" runat="server" FieldName="AcceptCookiesText" />
                </asp:LinkButton>
                <asp:LinkButton ID="lbDeclineCookiesText" runat="server" OnClick="lbDeclineCookiesText_Click">
                    <sc:FieldRenderer ID="frDeclineCookiesText" runat="server" FieldName="DeclineCookiesText" />
                </asp:LinkButton>
                <asp:HyperLink ID="hlFindOutMore" runat="server"></asp:HyperLink>
                <div id="cookieMessageClose">
                    <asp:LinkButton ID="lbClose" runat="server" OnClick="lbAcceptCookiesText_Click">Close</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>