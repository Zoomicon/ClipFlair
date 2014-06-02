<%@ Control 
Language="C#"
AutoEventWireup="true"
%>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register TagPrefix="MonoX" TagName="MembershipNavigation" Src="~/App_MasterPages/ClipFlair/MembershipNavigation.ascx" %>
<header>
    <div class="header-wrapper">
            <div class="header">
                <a href='<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Default.aspx")) %>' class="logo"><img runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.logo_png %>" alt="MonoX" /></a>             
                <div class="membership-holder">
                    <MonoX:MembershipNavigation runat="server" ID="ctlMemership" />
                </div>
            </div>            
    </div>
</header>