<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageFooter.ascx.cs" Inherits="MonoSoftware.MonoX.MasterPages.Mobile.PageFooter" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<style type="text/css">	
        .nav-glyph .ui-btn .ui-btn-inner { padding-top: 40px !important; }
        .nav-glyph .ui-btn .ui-icon { width: 30px!important; height: 30px!important; margin-left: -15px !important; box-shadow: none!important; -moz-box-shadow: none!important; -webkit-box-shadow: none!important; -webkit-border-radius: none !important; border-radius: none !important; }
        .monoxMobileProfile .ui-icon { background:  url(<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/App_Themes/Mobile/img/111-user.png") %>) 50% 50% no-repeat; background-size: 24px 21px; }
        .monoxMobileLogout .ui-icon { background:  url(<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/App_Themes/Mobile/img/02-redo.png") %>) 50% 50% no-repeat; background-size: 30px 26px;  }
        .monoxMobileLogin .ui-icon { background:  url(<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/App_Themes/Mobile/img/30-key.png") %>) 50% 50% no-repeat;  background-size: 12px 26px; }
        .monoxMobileRegister .ui-icon { background:  url(<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/App_Themes/Mobile/img/40-inbox.png") %>) 50% 50% no-repeat;  background-size: 24px 24px; }
 </style> 
	
<div data-role="footer" class="nav-glyph"> 
    <div data-role="navbar" class="nav-glyph" data-theme="b"> 
    <ul> 
        <asp:PlaceHolder runat="server" ID="plhAnonymous">
        <li><a href='<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/MonoX/Mobile/Login.aspx") %>' rel="external" class="monoxMobileLogin" data-icon="custom"><%= MobileResources.Login %></a></li> 
        <asp:PlaceHolder runat="server" ID="plhRegistration" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>">
        <li><a href='<%= MonoSoftware.Web.UrlFormatter.ResolveUrl("~/MonoX/Mobile/Register.aspx") %>' class="monoxMobileRegister" data-icon="custom"><%= MobileResources.Register %></a></li>
        </asp:PlaceHolder>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plhLoggedIn">
        <li><asp:LinkButton runat="server" CssClass="monoxMobileLogout" data-icon="custom" Text="<% $Code:MobileResources.Logout %>" ID="btnLogout"></asp:LinkButton></li>
        <li><a href="<%= GetProfileUrl() %>" class="monoxMobileProfile" data-icon="custom"><%= MobileResources.Profile %></a></li> 
        </asp:PlaceHolder>
    </ul> 
    </div> 
</div>