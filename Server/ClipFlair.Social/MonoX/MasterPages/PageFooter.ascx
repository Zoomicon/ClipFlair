<%@ Control 
Language="C#"
AutoEventWireup="true"
CodeBehind="PageFooter.ascx.cs"
Inherits="MonoSoftware.MonoX.MasterPages.PageFooter" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>

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
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Pages/PrivacyPolicy.aspx") %>' runat="server"><%= PageResources.PageFooter_PrivacyPolicy %></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Pages/TermsOfUse.aspx") %>' runat="server"><%= PageResources.PageFooter_TermsOfUse %></a></asp:HyperLink></li>
		    </ul>
    	    <ul class="span3">
        	    <li><h2><%= PageResources.PageFooter_GeneralInfo %> & Tools</h2></li>
                <li><a href="http://clipflair.net"><%= PageResources.PageFooter_Home %></a></li>
                <li><a href="http://clipflair.net/overview"><%= PageResources.PageFooter_About%>&nbsp;&rsaquo;</a></li>
                <li><a href="http://gallery.clipflair.net/activity">ClipFlair Gallery &rsaquo;</a></li>
                <li><a href="http://studio.clipflair.net">ClipFlair Studio &rsaquo;</a></li>                 
		    </ul>
    	    <ul class="span3">
        	    <li><h2><%= PageResources.PageFooter_SocialNetworking %></h2></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/Dashboard.aspx") %>' runat="server">Community</a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Blog.aspx") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingBlog %></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/Groups/GroupList/") %>' runat="server"><%= PageResources.PageFooter_SocialNetworkingGroups %></a></li>
                <li><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/SocialNetworking/Discussion.aspx") %>' runat="server">Forums</a></li>
		    </ul>
            <ul class="span3">
                <li><h2><%= PageResources.PageFooter_ConnectWithUs %></h2></li>
                <li class="contact"><a href='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/MonoX/Pages/Contact.aspx") %>' runat="server">Contact</a></li>
                <li class="facebook"><a href="https://www.facebook.com/ClipFlair" target="ClipFlair_Facebook">Facebook</a></li>
                <li class="twitter"><a href="https://twitter.com/ClipFlair" target="ClipFlair_Twitter">Twitter</a></li>
		            <li class="slideshare"><a href="http://www.slideshare.net/ClipFlair" target="ClipFlair_SlideShare">SlideShare</a></li>
            </ul>
        </div>
    </div>
    
    <div class="copyright">
        <div class="container">
            <div class="row-fluid">

                <div class="logo-eu">
                    <img runat="server" src="~/App_Themes/ClipFlair/img/logo-eu.jpg" alt="Lifelong Learning Programme" class="footer-logo" />
                    <p>This project has been funded with support from the European Commission. This publication reflects the views only of the author, and the Commission cannot be held responsible for any use which may be made of the information contained therein.</p>
                </div>

                <p style="color:white">
                Developed by: <a href="http://Zoomicon.com" target="Zoomicon" style="color:grey">Zoomicon</a> &amp; <a href="http://www.cti.gr" target="CTI" style="color:grey">CTI</a>
                <a id="A1" runat="server" href="http://monox.mono-software.com" class="powered-by">
                  <img runat="server" src="~/App_Themes/trafilm/img/PoweredBy/MonoXButton1.gif" alt="Powered by MonoX" />
                </a>
                </p>   
                
            </div>
        </div>
    </div>
</footer>