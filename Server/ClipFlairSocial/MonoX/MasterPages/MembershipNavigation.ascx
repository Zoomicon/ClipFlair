<%@ Control 
    Language="C#"
    AutoEventWireup="true"
    CodeBehind="MembershipNavigation.ascx.cs"
    Inherits="MonoSoftware.MonoX.MasterPages.MembershipNavigation" %>
<%@ Register Src="~/MonoX/controls/SeoLoginStatus.ascx" TagPrefix="monox" TagName="SeoLoginStatus" %>
<%@ Import Namespace="MonoSoftware.MonoX.Resources"  %>

<div class="login-status-register-holder login-status-register">
    <div id="panLoginName" runat="server" class="login-status-registerr user-icon">
        <img id="imgUserIcon" runat=server src="<%$ Code: MonoSoftware.MonoX.Paths.App_Themes.img.user_icon_png %>" alt="user-icon" class="user-icon" />
        <asp:LoginName ID="loginName" runat="server" CssClass="login-status-name"  /> 
    </div>   
    <asp:PlaceHolder ID="panAnonymousTemplate" runat="server">
        <asp:HyperLink runat="server" ID="lnkRegister" Visible="<% $Code: MonoSoftware.MonoX.ApplicationSettings.EnableUserRegistration %>" CssClass="login-nav-register"></asp:HyperLink>
        <monox:SeoLoginStatus ID="loginStatus1" runat="server" CssClass="login-nav-loginstatus" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="panLoggedInTemplate" runat="server">
        <monox:SeoLoginStatus runat="server" ID="loginStatus2" CssClass="login-nav-loginstatus" />
        <asp:HyperLink runat="server" ID="lnkProfile" CssClass="login-nav-profile"></asp:HyperLink>            
        <asp:HyperLink runat="server" ID="lnkMessages" CssClass="message-box" >
            <span class="active-message"></span>
        </asp:HyperLink>    
    </asp:PlaceHolder>
</div>