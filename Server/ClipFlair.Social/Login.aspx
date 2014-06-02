<%@ Page 
    Language="C#" 
    MasterPageFile="~/App_MasterPages/ClipFlair/Login.master" 
    AutoEventWireup="true" 
    Inherits="MonoSoftware.MonoX.Pages.Login" 
    Codebehind="Login.aspx.cs" %>
  
<%@ Import Namespace="MonoSoftware.MonoX.Resources"%>
<%@ MasterType TypeName="MonoSoftware.MonoX.BaseMasterPage" %>    
<%@ Register TagPrefix="MonoX" TagName="Login" Src="~/MonoX/ModuleGallery/LoginModule.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="LoginSocial" Src="~/MonoX/ModuleGallery/LoginSocial.ascx" %>
<%@ Register TagPrefix="MonoX" TagName="MembershipNavigation" Src="~/MonoX/MasterPages/MembershipNavigation.ascx" %>

<asp:Content ContentPlaceHolderID="cp" Runat="Server">
   
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
    <div class="fancybox-container">
        <div class="row-fluid">
            <div class="span6 clearfix">       
                <MonoX:MembershipNavigation runat="server" ID="ctlMemership" LoginStatusLogoutText="&nbsp;" />
                <MonoX:Login runat="server" ID="ctlLogin" Width="100%"  />
            </div>
            <div class="span6 clearfix" style="position: relative;"> 
                <div id="rowRPX" runat="server" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration && !Page.User.Identity.IsAuthenticated %>">
                    <!--CLIPFLAIR<div class="or-use">
                        <hr />
                        <div><%= PageResources.Login_Or %></div>
                    </div>-->
                    <div class="user-account login-social" >
                        <MonoX:LoginSocial runat="server" ID="ctlLoginSocial" />
                        <div class="italic-style"><asp:Literal ID="Literal1" runat="server" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>" Text="Do not use social login if you've already registered directly with ClipFlair Social: enter your User name and Password instead and click the Log in button"></asp:Literal></div>
                    </div>
                </div>
            </div>
        </div> 
    </div>
</asp:Content>
