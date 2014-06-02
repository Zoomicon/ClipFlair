<%@ Control 
Language="C#"
AutoEventWireup="true" 
Codebehind="Login.aspx.cs"
%>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>
<%@ Register TagPrefix="MonoX" TagName="Login" Src="~/MonoX/ModuleGallery/LoginModule.ascx" %>
<header>
    <div class="header-wrapper">
            <div class="header">
                <a href='<%= MonoSoftware.Web.UrlFormatter.ResolveServerUrl(MonoSoftware.MonoX.Utilities.LocalizationUtility.RewriteLink("~/Default.aspx")) %>' class="logo"><img runat="server" src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.logo_png %>" alt="MonoX" /></a>             
                <div class="empty-top-section">
                    <div class="container-fluid-small">
                        <asp:PlaceHolder ID="plhAuthorizationMessage" runat="server" Visible="false">
                            <div class="error">
                                <div><img src="<%= ResolveUrl(MonoSoftware.MonoX.Paths.App_Themes.img.Error_png) %>" alt="Error" /></div>
                                <%= ErrorMessages.Authorization_Login %>
                            </div>
                        </asp:PlaceHolder>
                    </div>
                </div>
                <div class="current-login">                
                    <div class="login-left-section">
                        <MonoX:Login runat="server" ID="ctlLogin" Width="100%"  />
                    </div>
                </div>
            </div>            
    </div>
</header>