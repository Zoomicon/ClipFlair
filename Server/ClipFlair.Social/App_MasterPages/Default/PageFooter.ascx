<%@ Control 
Language="C#"
AutoEventWireup="true"
CodeBehind="PageFooter.ascx.cs"
Inherits="MonoSoftware.MonoX.MasterPages.PageFooter" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register TagPrefix="MonoX" TagName="DemoIndicator" Src="~/MonoX/MasterPages/DemoIndicator.ascx" %>
<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>

<MonoX:DemoIndicator ID="ctlDemo" runat="server"></MonoX:DemoIndicator>
<footer>
    <div class="container">
        <div class="row-fluid">
    	    <ul class="span3">
        	    <li><h2><%= PageResources.PageFooter_SiteMembership %></h2></li>
                <li style='<%= HttpContext.Current.User.Identity.IsAuthenticated ? "" : "display:none;" %>'><asp:LoginName ID="loginName" runat="server" /></li>
                <li><monox:SeoLoginStatus ID="loginStatus" runat="server" /></li>
                <asp:LoginView runat="server" ID="loginView">
                    <AnonymousTemplate>
                        <li><asp:HyperLink runat="server" ID="lnkRegister" Text="<% $Code:DefaultResources.Login_RegisterInvitation %>" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Membership/Register.aspx") %>'></asp:HyperLink></li>
                    </AnonymousTemplate>
                </asp:LoginView>
                <li><asp:HyperLink runat="server" ID="lnkPrivacyPolicy" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.RewrittenUrlBuilder.GetContentPageUrl(MonoSoftware.MonoX.RewrittenPaths.ContentPage, "PrivacyPolicy").Url) %>'><%= PageResources.PageFooter_PrivacyPolicy %></asp:HyperLink></li>
                <li><asp:HyperLink runat="server" ID="lnkTermsOfUse" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.RewrittenUrlBuilder.GetContentPageUrl(MonoSoftware.MonoX.RewrittenPaths.ContentPage, "TermsOfUse").Url) %>'><%= PageResources.PageFooter_TermsOfUse %></asp:HyperLink></li>
		    </ul>
    	    <ul class="span3">
        	    <li><h2><%= PageResources.PageFooter_GeneralInfo %></h2></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/") %>' runat="server"><%= PageResources.PageFooter_Home %></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/Features.aspx") %>' runat="server"><%= PageResources.PageFooter_About%></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/AdditionalResources.aspx") %>' runat="server"><%= PageResources.PageFooter_AdditionalResources %></a></li>
                <li><asp:HyperLink runat="server" ID="lnkLicensing" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.RewrittenUrlBuilder.GetContentPageUrl(MonoSoftware.MonoX.RewrittenPaths.ContentPage, "Licensing").Url) %>'><%= PageResources.PageFooter_Licensing%></asp:HyperLink></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/Contact.aspx") %>' runat="server">Contact</a></li>
		    </ul>
    	    <ul class="span3">
        	    <li><h2><%= PageResources.PageFooter_SocialNetworking %></h2></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/Dashboard.aspx") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingHome%></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/blog/posts/MonoX/") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingBlog %></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/Groups/GroupList/") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingGroups%></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/PhotoGallery.aspx") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingPhotos %></a></li>
		    </ul>
            <ul class="span3">
                <li><h2><%= PageResources.PageFooter_ConnectWithUs %></h2></li>
                <li class="facebook"><a href="http://www.facebook.com/pages/Mono-Software-Ltd/192570507442137"><%= PageResources.PageFooter_FacebookFan %></a></li>
                <li class="twitter"><a href="https://twitter.com/monosoftware"><%= PageResources.PageFooter_TwitterFollow %></a></li>
                <li class="linkedin"><a href="http://hr.linkedin.com/company/mono-software-ltd."><%= PageResources.PageFooter_LinkedInConnect %></a></li>
            </ul>
        </div>
    </div>
    <div class="copyright">
        <a id="A1" runat="server" href="http://monox.mono-software.com" class="powered-by">
            <img id="Img1" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.powered_by_png %>" alt="Powered by MonoX" />
        </a>
        <span><%= PageResources.PageFooter_Copyright%> &#169;2012 <a href="http://www.mono-software.com">Mono Ltd.</a></span>
    </div>
</footer>