<%@ Control 
Language="C#"
AutoEventWireup="true"
CodeBehind="PageFooter.ascx.cs"
Inherits="MonoSoftware.MonoX.MasterPages.PageFooter" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>

<div class="footer-wrapper">
	<div class="footer">
    	<ul>
        	
            <li><span class="copyright"><%= PageResources.PageFooter_Copyright%> &#169;2011 <a href="#"><strong>ClipFlair</strong></a></span>&nbsp; /&nbsp;    <asp:HyperLink runat="server" ID="lnkPrivacyPolicy" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.RewrittenUrlBuilder.GetContentPageUrl(MonoSoftware.MonoX.RewrittenPaths.ContentPage, "PrivacyPolicy").Url) %>'><%= PageResources.PageFooter_PrivacyPolicy %></asp:HyperLink> &nbsp; : &nbsp; <asp:HyperLink runat="server" ID="lnkTermsOfUse" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink(MonoSoftware.MonoX.RewrittenUrlBuilder.GetContentPageUrl(MonoSoftware.MonoX.RewrittenPaths.ContentPage, "TermsOfUse").Url) %>'><%= PageResources.PageFooter_TermsOfUse %></asp:HyperLink></li>
            <!--<li><asp:LoginName ID="loginName" runat="server" /></li>-->
            <li class="hidden"><monox:SeoLoginStatus ID="loginStatus" runat="server" /></li>
            <!--<asp:LoginView runat="server" ID="loginView">
                <AnonymousTemplate>
                    <li><asp:HyperLink runat="server" ID="lnkRegister" Text="<% $Code:DefaultResources.Login_RegisterInvitation %>" NavigateUrl='<% $Code: MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Membership/Register.aspx") %>'></asp:HyperLink></li>
                </AnonymousTemplate>
            </asp:LoginView>-->
            <li class="align-right"><a href="https://twitter.com/ClipFlair" title="visit our twitter account" class="twitter-link"></a></li>
            <li class="align-right"><a href="http://www.facebook.com/ClipFlair" title="visit our facebook page" class="facebook-link"></a></li>
            
            

		</ul>
    </div>   
    <div class="footer-logos">
            
                
            
                    <div class="EU-logo"></div>
                    <div class="EU-notice">This project has been funded with support from the European Commission. <br/>This publication reflects the views only of the author, and the Commission<br/> cannot be held responsible for any use which may be made of the information<br/> contained therein.</div>
                    <div class="monox-logo">
                        <a id="A1" runat="server" href="http://monox.mono-software.com" class="logo"><img id="Img1" runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.powered_by_png %>" style="width: 90px;" alt="Powered by MonoX" /></a>
                    </div>
   	
	<div>
		    
</div>
        
        
    
</div>
